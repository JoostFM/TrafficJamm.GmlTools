using Microsoft.Extensions.Logging;
using System.IO.Abstractions;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using TrafficJamm.GmlToFileSystem.Gml.Helpers;
using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem.Gml;

public class DbDumpFileService(IFileSystem fileSystem, ILogger<DbDumpFileService> logger) : IDbDumpFileService
{
    private const int ItemTypeAudio = 10;

    public List<TrackInfoModel> ConvertToTrackInfoList(List<GMLDatabaseItem> gmlAudioItems)
    {
        var returnValue = new List<TrackInfoModel>();

        foreach (var item in gmlAudioItems)
        {
            try
            {
                var trackInfo = new TrackInfoModel
                {
                    Artist = item.ItemInfo.ItemInterpret,
                    Title = item.ItemInfo.ItemTitle,
                    CategoryPath = ParserHelpers.ParseGmlPath(item.CategoryInfo.CategoryNamePath),
                    Filename = ParserHelpers.ParseFileName(item.ItemInfo.ItemInterpret, item.ItemInfo.ItemTitle),
                    DurationMs = (int)item.ItemInfo.ItemDuration,
                    DateAdded = ParserHelpers.ParseDateTimeOffset(item.ItemFiles.FileAudio.FileTimeStampCreated),
                    DateModified = ParserHelpers.ParseDateTimeOffset(item.ItemFiles.FileAudio.FileTimeStampModified),
                    DateAccessed = ParserHelpers.ParseDateTimeOffset(item.ItemFiles.FileAudio.FileTimeStampAccessed),
                    Cues = GetCues(item.ItemMix),
                    Metadata = new Dictionary<string, string> {
                        { "Ref", item.ItemReferences.RefReference },
                        { "ItemId", item.ItemInfo.ItemId.ToString() }
                    }
                };

                returnValue.Add(trackInfo);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex, "Unexpected Value in item {ItemId} : {Exception}", item.ItemInfo.ItemId, ex.Message);
            }
        }
        logger.LogInformation("Converted {Count} GML items to TrackInfoModel list", returnValue.Count);
        return returnValue;
    }

    private static CueSettings GetCues(GMLDatabaseItemItemMix itemMix)
        => new()
        {
            MixTimeFadeIn = (int)itemMix.MixTimeFadeIn,
            MixTimeFadeOut = (int)itemMix.MixTimeFadeOut,
            MixPosBegin = (int)itemMix.MixPosBegin,
            MixPosEnd = (int)itemMix.MixPosEnd,
            MixPosNext = (int)itemMix.MixPosNext,
            MixPosIntro = (int)itemMix.MixPosIntro,
            MixPosOutro = (int)itemMix.MixPosOutro
        };

    public string ExportToGmlTrackIndex(List<TrackInfoModel> trackList)
    {
        var content = JsonSerializer.Serialize(trackList, Constants.JsonOptions);

        var filename = Path.Combine(Constants.OutputFilePath, $"GmlTrackIndex_{DateTime.Now:yyyyMMdd_HHmmss}.json");

        fileSystem.File.WriteAllText(filename, content);

        return filename;
    }

    public List<GMLDatabaseItem> FilterAudioItems(List<GMLDatabaseItem> gmlItems)
    {
        return [.. gmlItems
            .Where(item => item.ItemInfo != null
                && item.ItemInfo.ItemType == ItemTypeAudio
                && item.ItemFiles.FileAudio.FilePathId == 1
                && item.ItemFiles.FileAudio.FileAttrText != "[Invalid]")];
    }

    public string GetDbDumpFilePath(DbDumpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!fileSystem.Directory.Exists(request.DbDumpFileLocationPath))
        {
            throw new DirectoryNotFoundException($"The provided directory path '{request.DbDumpFileLocationPath}' does not exist.");
        }

        if (string.IsNullOrWhiteSpace(request.DbDumpFileName))
        {
            var dbDumpFiles = fileSystem.Directory.GetFiles(request.DbDumpFileLocationPath, "*.xml");
            if (dbDumpFiles.Length == 0)
            {
                throw new FileNotFoundException($"No XML files found in the directory '{request.DbDumpFileLocationPath}'.");
            }
            return dbDumpFiles[0];
        }

        return Path.Combine(request.DbDumpFileLocationPath, request.DbDumpFileName);
    }

    public List<GMLDatabaseItem> GetGmlItemsFromDbDumpFile(string dumpFilePath)
    {
        using XmlReader reader = XmlReader.Create(dumpFilePath);
        XmlSerializer serializer = new(typeof(GmlModel));

        // Check if the root element can be deserialized
        if (!serializer.CanDeserialize(reader))
        {
            throw new InvalidOperationException("XML structure does not match expected GML format");
        }

        if (serializer.Deserialize(reader) is not GmlModel gmlModel)
        {
            throw new InvalidOperationException("Failed to deserialize the GML model from the XML file.");
        }

        if (gmlModel.AppSpecificInfo == null || gmlModel.AppSpecificInfo.Length == 0)
        {
            throw new InvalidOperationException("Xml contains no items in the AppSpecificInfo section.");
        }

        return [.. gmlModel.AppSpecificInfo];
    }
}
