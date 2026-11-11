using GmlToFileSystem.Library.Gml.Mappers;
using GmlToFileSystem.Library.Gml.Models;
using GmlToFileSystem.Library.Gml.Workers;
using GmlToFileSystem.Library.Models;
using Microsoft.Extensions.Logging;

namespace GmlToFileSystem.Library.Gml.Services;

public class GmlDumpFileService(IGmlDumpFilePathWorker gmlDumpFilePathWorker, IGmlItemReader gmlItemReader, ILogger<GmlDumpFileService> logger) : IGmlDumpFileService
{
    public async Task<List<TrackInfoModel>> ConvertGmlDumpFileToTrackListAsync(string gmlDumpFilePath)
    {
        var dbDumpFilePath = await gmlDumpFilePathWorker.GetDbDumpFilePathAsync(gmlDumpFilePath);
        var gmlAudioItems = await gmlItemReader.ReadGmlAudioItemsAsync(dbDumpFilePath);
        var trackInfoList = ConvertToTrackInfoList(gmlAudioItems);

        return trackInfoList;
    }

    internal List<TrackInfoModel> ConvertToTrackInfoList(List<GMLDatabaseItem> gmlAudioItems)
    {
        var returnValue = new List<TrackInfoModel>();

        foreach (var item in gmlAudioItems)
        {
            try
            {
                var trackInfo = GmlDatabaseItemToTrackInfoModelMapper.Map(item);

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
}
