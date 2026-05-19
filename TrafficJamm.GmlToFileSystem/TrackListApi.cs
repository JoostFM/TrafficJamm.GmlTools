using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO.Abstractions;
using System.Text.Json;
using TrafficJamm.GmlToFileSystem.Models;
using TrafficJamm.GmlToFileSystem.Services;

namespace TrafficJamm.GmlToFileSystem;

public class TrackListApi(IOptions<GmlToFileSystemSettings> options,
    ILogger<TrackListApi> logger,
    ITrackListService trackListService,
    IFileSystem fileSystem) : ITrackListApi
{
    private readonly GmlToFileSystemSettings _settings = options.Value;
    public async Task<Result> TransferGmlFilesToFileSystem(string gmlTrackListFile)
    {
        var content = await fileSystem.File.ReadAllTextAsync(gmlTrackListFile);
        var gmltrackListFilePath = JsonSerializer.Deserialize<List<TrackInfoModel>>(content, Constants.JsonOptions);
        var fileSystemTrackListFile = trackListService.GetFileSystemTrackList(_settings.AudioTargetLocation);

        var trackListTransferList = trackListService.GetTrackListTransferList(gmltrackListFilePath, fileSystemTrackListFile);

        foreach (var trackInfo in trackListTransferList)
        {
            if (!trackInfo.Metadata.ContainsKey("GmlFilename") || string.IsNullOrEmpty(trackInfo.Metadata["GmlFilename"]))
            {
                logger.LogInformation("Skip {Filename} track with missing reference", trackInfo.Reference);
                continue;
            }

            var gmlFilename = trackInfo.Metadata["GmlFilename"];
            var source = Path.Combine(_settings.AudioSourceLocation, gmlFilename);

            if (!fileSystem.File.Exists(source))
            {
                logger.LogWarning("Source file {Source} does not exist, skipping {Reference}", source, trackInfo.Reference);
                continue;
            }

            var destination = Path.Combine(_settings.AudioTargetLocation, trackInfo.Reference);
            // Haal alleen de map uit het doelpad
            var destDirectory = Path.GetDirectoryName(destination);

            // Maak de map aan als deze nog niet bestaat
            if (!string.IsNullOrEmpty(destDirectory) && !Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }

            fileSystem.File.Copy(source, destination, true);
            fileSystemTrackListFile.Add(trackInfo);
            logger.LogInformation("Copied {GmlFilename} to {Target}", gmlFilename, destination);

        }

        await fileSystem.File.WriteAllTextAsync(Path.Combine(_settings.AudioTargetLocation, Constants.FileSystemTrackListFile), JsonSerializer.Serialize(fileSystemTrackListFile, Constants.JsonOptions));
        await Task.CompletedTask;

        return new Result
        {
            Success = true,
            Message = "Message success"
        };
    }
}
