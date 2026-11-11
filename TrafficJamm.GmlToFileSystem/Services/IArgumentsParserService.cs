using TrafficJamm.GmlToFileSystem.Gml.Models;

namespace TrafficJamm.GmlToFileSystem.Services;

public interface IArgumentsParserService
{
    /// <summary>
    /// Process CLI arguments
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    DbDumpRequest ProcesCliArguments(string[] args);
}