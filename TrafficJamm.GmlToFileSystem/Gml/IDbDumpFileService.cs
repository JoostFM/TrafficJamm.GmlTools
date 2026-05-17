using TrafficJamm.GmlToFileSystem.Gml.Models;

namespace TrafficJamm.GmlToFileSystem.Gml;

public interface IDbDumpFileService
{
    string GetDbDumpFilePath(DbDumpRequest request);
}