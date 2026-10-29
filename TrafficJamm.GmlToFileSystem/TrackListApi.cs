using Microsoft.Extensions.Logging;
using TrafficJamm.GmlToFileSystem.Gml.Models;
using System.IO.Abstractions;
using System.Text.Json;
using TrafficJamm.GmlToFileSystem.Models;
using TrafficJamm.GmlToFileSystem.Services;

namespace TrafficJamm.GmlToFileSystem;

public class TrackListApi(ILogger<TrackListApi> logger,
    ITrackListService trackListService,
    IFileSystem fileSystem) : ITrackListApi
{
    public async Task<Result> TransferGmlFilesToFileSystem(DbDumpRequest request, string gmlTrackListFile)
    {
        var content = await fileSystem.File.ReadAllTextAsync(gmlTrackListFile);
        var gmltrackListFilePath = JsonSerializer.Deserialize<List<TrackInfoModel>>(content, Constants.JsonOptions);
        var fileSystemTrackListFile = trackListService.GetFileSystemTrackList(request.TargetFilePath);

        var trackListTransferList = trackListService.GetTrackListTransferList(gmltrackListFilePath, fileSystemTrackListFile);

        foreach (var trackInfo in trackListTransferList)
        {
            if (!trackInfo.Metadata.ContainsKey("GmlFilename") || string.IsNullOrEmpty(trackInfo.Metadata["GmlFilename"]))
            {
                logger.LogInformation("Skip {Filename} track with missing reference", trackInfo.Reference);
                continue;
            }

            var gmlFilename = trackInfo.Metadata["GmlFilename"];
            var source = Path.Combine(request.SourceFilePath, gmlFilename);

            if (!fileSystem.File.Exists(source))
            {
                logger.LogWarning("Source file {Source} does not exist, skipping {Reference}", source, trackInfo.Reference);
                continue;
            }

            var destination = Path.Combine(request.TargetFilePath, trackInfo.Reference);
            // Haal alleen de map uit het doelpad
            var destDirectory = Path.GetDirectoryName(destination);

            // Maak de map aan als deze nog niet bestaat
            if (!string.IsNullOrEmpty(destDirectory) && !Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            if (!fileSystem.File.Exists(destination))
            {
                fileSystem.File.Copy(source, destination, true);
                logger.LogInformation("Copied {GmlFilename} to {Target}", gmlFilename, destination);
            }
            else
            {
                logger.LogWarning("Already exists, skipping {GmlFilename} to '{Reference}' ", gmlFilename, trackInfo.Reference);
            }

            fileSystemTrackListFile.Add(trackInfo);

        }

        await fileSystem.File.WriteAllTextAsync(Path.Combine(request.TargetFilePath, Constants.FileSystemTrackListFile),
            JsonSerializer.Serialize(fileSystemTrackListFile, Constants.JsonOptions));
        await Task.CompletedTask;

        return new Result
        {
            Success = true,
            Message = "Message success"
        };
    }
}
