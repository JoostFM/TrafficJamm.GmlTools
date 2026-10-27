using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem;

public interface ITrackListApi
{
    Task<Result> TransferGmlFilesToFileSystem(DbDumpRequest request, string gmlTrackListFile);
}