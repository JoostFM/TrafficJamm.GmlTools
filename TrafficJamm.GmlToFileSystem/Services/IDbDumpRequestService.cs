using TrafficJamm.GmlToFileSystem.Gml.Models;

namespace TrafficJamm.GmlToFileSystem.Services;

public interface IDbDumpRequestService
{
    /// <summary>
    /// Process CLI arguments
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    DbDumpRequest CreateDbDumpRequest(string[] args);
}