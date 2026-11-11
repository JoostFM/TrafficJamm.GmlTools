using GmlToFileSystem.Library.Models;

namespace TrafficJamm.GmlToFileSystem.Workers
{
    public interface ITrackListFilterWorker
    {
        List<TrackInfoModel> GetItemsToUpdate(List<TrackInfoModel> gmltrackList, List<TrackInfoModel> fileSystemTrackList);
    }
}