using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem;

public interface IGmlApi
{
    Task<Result<string>> CreateTrackListFromXmlDbDumpGmlAsync(DbDumpRequest request);
}