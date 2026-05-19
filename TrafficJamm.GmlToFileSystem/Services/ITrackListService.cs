using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem.Services
{
    public interface ITrackListService
    {
        List<TrackInfoModel> GetFileSystemTrackList(string audioTargetLocation);
        List<TrackInfoModel> GetTrackListTransferList(List<TrackInfoModel> gmltrackList, List<TrackInfoModel> fileSystemTrackList);
    }
}