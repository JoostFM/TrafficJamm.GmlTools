using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem;

public interface ITrackListApi
{
    /// <summary>
    /// Compare GmlTrackInfo with existing files, copy the missing files with 
    /// </summary>
    /// <param name="sourceFilePath"></param>
    /// <param name="targetFilePath"></param>
    /// <param name="gmlTrackListFile"></param>
    /// <returns></returns>
    Task<Result> TransferGmlFilesToFileSystem(string sourceFilePath, string targetFilePath, string gmlTrackListFile);
}