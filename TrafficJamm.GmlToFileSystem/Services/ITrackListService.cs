using GmlToFileSystem.Library.Models;

namespace TrafficJamm.GmlToFileSystem.Services;

public interface ITrackListService
{
    /// <summary>
    /// Read or create the Trackinfo for the File System/ GmlIndex
    /// </summary>
    /// <param name="audioTargetLocation"></param>
    /// <returns></returns>
    List<TrackInfoModel> GetFileSystemTrackList(string audioTargetLocation);

    /// <summary>
    /// Compare The SourceFiles from Gml with the existing files in the TargetFileSystem 
    /// and return a list of items that are not yet in the target
    /// </summary>
    /// <param name="gmltrackList"></param>
    /// <param name="fileSystemTrackList"></param>
    /// <returns></returns>
    List<TrackInfoModel> GetTrackListTransferList(List<TrackInfoModel> gmltrackList, List<TrackInfoModel> fileSystemTrackList);
}