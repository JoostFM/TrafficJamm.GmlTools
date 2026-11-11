using GmlToFileSystem.Library.Models;
using System.IO.Abstractions;
using System.Text.Json;
using TrafficJamm.GmlToFileSystem.Models;
using TrafficJamm.GmlToFileSystem.Workers;

namespace TrafficJamm.GmlToFileSystem.Services;

public class TrackListService(ITrackListFilterWorker trackListFilterWorker, IFileSystem fileSystem) : ITrackListService
{
    public List<TrackInfoModel> GetFileSystemTrackList(string audioTargetLocation)
    {
        List<TrackInfoModel> fileSystemTrackList = [];
        var fileSystemTrackListFile = Path.Combine(audioTargetLocation, Constants.FileSystemTrackListFile);

        if (fileSystem.File.Exists(fileSystemTrackListFile))
        {
            var content = fileSystem.File.ReadAllText(fileSystemTrackListFile);
            fileSystemTrackList = JsonSerializer.Deserialize<List<TrackInfoModel>>(content, Constants.JsonOptions);
        }
        else
        {
            fileSystem.File.WriteAllText(fileSystemTrackListFile, JsonSerializer.Serialize(fileSystemTrackList, Constants.JsonOptions));
        }

        // Implementation for retrieving the file system track list
        return fileSystemTrackList;
    }

    public List<TrackInfoModel> GetTrackListTransferList(List<TrackInfoModel> gmltrackList, List<TrackInfoModel> fileSystemTrackList)
        => trackListFilterWorker.GetItemsToUpdate(gmltrackList, fileSystemTrackList);
}
