using Microsoft.Extensions.Logging;
using System.IO.Abstractions;

namespace GmlToFileSystem.Library.Gml.Workers;

public class GmlDumpFilePathWorker(IFileSystem fileSystem, ILogger<GmlDumpFilePathWorker> logger) : IGmlDumpFilePathWorker
{
    public async Task<string> GetDbDumpFilePathAsync(string gmlDumpFilePath)
    {
        if (fileSystem.File.Exists(gmlDumpFilePath))
        {
            return gmlDumpFilePath;
        }

        if (!fileSystem.Directory.Exists(gmlDumpFilePath))
        {
            throw new DirectoryNotFoundException($"The provided directory path '{gmlDumpFilePath}' does not exist.");
        }

        var dbDumpFiles = await GetFilesAsync(gmlDumpFilePath, "*.xml");
        if (dbDumpFiles.Count == 0)
        {
            throw new FileNotFoundException($"No XML files found in the directory '{gmlDumpFilePath}'.");
        }

        logger.LogInformation("Found {Count} XML files in the directory '{Directory}'", dbDumpFiles.Count, gmlDumpFilePath);

        var dbDumpFilePath = dbDumpFiles
            .OrderByDescending(file => fileSystem.File.GetLastWriteTimeUtc(file))
            .First();

        logger.LogInformation("Selected the latest XML file '{File}' from the directory '{Directory}'", dbDumpFilePath, gmlDumpFilePath);

        return dbDumpFilePath;
    }

    private async Task<IReadOnlyList<string>> GetFilesAsync(string path, string searchPattern, CancellationToken cancellationToken = default)
    {
        return await Task.Run(
            () => fileSystem.Directory.GetFiles(path, searchPattern),
            cancellationToken);
    }
}
