using GmlToFileSystem.Library.Models;

namespace TrafficJamm.GmlToFileSystem.Workers
{
    public class TrackListFilterWorker : ITrackListFilterWorker
    {
        public List<TrackInfoModel> GetItemsToUpdate(List<TrackInfoModel> gmltrackList, List<TrackInfoModel> fileSystemTrackList)
        {
            var fileSystemTrackLookup = fileSystemTrackList
                .GroupBy(track => track.Reference)
                .ToDictionary(group => group.Key, group => group.First());

            return [.. gmltrackList
                .Where(gmlTrack =>
                    !fileSystemTrackLookup.TryGetValue(gmlTrack.Reference, out var fileSystemTrack)
                    || gmlTrack.DateModified > fileSystemTrack.DateModified)];
        }
    }
}
