using TrafficJamm.GmlToFileSystem.Gml.Models;

namespace TrafficJamm.GmlToFileSystem.Services;

public interface IDbDumpRequestService
{
    DbDumpRequest CreateDbDumpRequest(string? dumpFileName, string? sourceFilePath, string? targetFilePath);
}