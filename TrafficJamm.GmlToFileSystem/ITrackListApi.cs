using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem;

public interface ITrackListApi
{
    Task<Result> TransferGmlFilesToFileSystem(string gmlTrackListFile);
}