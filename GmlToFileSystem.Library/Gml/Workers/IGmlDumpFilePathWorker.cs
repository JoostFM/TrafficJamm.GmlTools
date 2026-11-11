namespace GmlToFileSystem.Library.Gml.Workers;

public interface IGmlDumpFilePathWorker
{
    /// <summary>
    /// Gets the full path to the most recentdatabase dump file based on the gmlDumpFilePath
    /// </summary>
    /// <param name="gmlDumpFilePath"></param>
    /// <returns></returns>
    Task<string> GetDbDumpFilePathAsync(string gmlDumpFilePath);
}