using System.IO.Abstractions;
using TrafficJamm.GmlToFileSystem.Gml.Models;

namespace TrafficJamm.GmlToFileSystem.Gml;

public class DbDumpFileService(IFileSystem fileSystem ) : IDbDumpFileService
{
    public string GetDbDumpFilePath(DbDumpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if(!fileSystem.Directory.Exists(request.DbDumpFileLocationPath) )
        {
            throw new DirectoryNotFoundException($"The provided directory path '{request.DbDumpFileLocationPath}' does not exist.");
        }

        if (string.IsNullOrWhiteSpace(request.DbDumpFileName))
        {
            var dbDumpFiles = fileSystem.Directory.GetFiles(request.DbDumpFileLocationPath, "*.xml");
            if (dbDumpFiles.Length == 0)
            {
                throw new FileNotFoundException($"No XML files found in the directory '{request.DbDumpFileLocationPath}'.");
            }
            return dbDumpFiles[0];
        }

        return Path.Combine(request.DbDumpFileLocationPath, request.DbDumpFileName);
    }
}
