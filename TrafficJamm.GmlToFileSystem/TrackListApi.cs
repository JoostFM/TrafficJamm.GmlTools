using GmlToFileSystem.Library.Models;
using Microsoft.Extensions.Logging;
using System.IO.Abstractions;
using System.Text.Json;
using TrafficJamm.GmlToFileSystem.Models;
using TrafficJamm.GmlToFileSystem.Services;

namespace TrafficJamm.GmlToFileSystem;

public class TrackListApi(ILogger<TrackListApi> logger,
    ITrackListService trackListService,
    IFileSystem fileSystem) : ITrackListApi
{
    public async Task<Result> TransferGmlFilesToFileSystem(string sourceFilePath, string targetFilePath, string gmlTrackListFile)
    {
        var content = await fileSystem.File.ReadAllTextAsync(gmlTrackListFile);
        var gmltrackListFilePath = JsonSerializer.Deserialize<List<TrackInfoModel>>(content, Constants.JsonOptions);
        var fileSystemTrackListFile = trackListService.GetFileSystemTrackList(targetFilePath);

        var trackListTransferList = trackListService.GetTrackListTransferList(gmltrackListFilePath, fileSystemTrackListFile);
        
        logger.LogInformation("Found {Count} new items to transfer from GML to FileSystem", trackListTransferList.Count);

        foreach (var trackInfo in trackListTransferList)
        {
            if (!trackInfo.Metadata.ContainsKey("GmlFilename") || string.IsNullOrEmpty(trackInfo.Metadata["GmlFilename"]))
            {
                logger.LogInformation("Skip {Filename} track with missing reference", trackInfo.Reference);
                continue;
            }

            var gmlFilename = trackInfo.Metadata["GmlFilename"];
            var source = Path.Combine(sourceFilePath, gmlFilename);

            if (!fileSystem.File.Exists(source))
            {
                logger.LogWarning("Source file {Source} does not exist, skipping {Reference}", source, trackInfo.Reference);
                continue;
            }

            var destination = Path.Combine(targetFilePath, trackInfo.Reference);
            var existingTrackIndex = fileSystemTrackListFile.FindIndex(existingTrack => existingTrack.Reference == trackInfo.Reference);
            var destinationExists = fileSystem.File.Exists(destination);
            var destDirectory = Path.GetDirectoryName(destination);

            if (!string.IsNullOrEmpty(destDirectory) && !fileSystem.Directory.Exists(destDirectory))
            {
                fileSystem.Directory.CreateDirectory(destDirectory);
            }

            if (existingTrackIndex >= 0 || !destinationExists)
            {
                fileSystem.File.Copy(source, destination, overwrite: true);
                logger.LogInformation("Copied {GmlFilename} to {Target}", gmlFilename, destination);
            }
            else
            {
                logger.LogWarning("Already exists, skipping {GmlFilename} to '{Reference}' ", gmlFilename, trackInfo.Reference);
            }

            if (existingTrackIndex >= 0)
            {
                fileSystemTrackListFile[existingTrackIndex] = trackInfo;
            }
            else
            {
                fileSystemTrackListFile.Add(trackInfo);
            }
        }

        await fileSystem.File.WriteAllTextAsync(Path.Combine(targetFilePath, Constants.FileSystemTrackListFile),
            JsonSerializer.Serialize(fileSystemTrackListFile, Constants.JsonOptions));

        return new Result
        {
            Success = true,
            Message = "Message success"
        };
    }
}
