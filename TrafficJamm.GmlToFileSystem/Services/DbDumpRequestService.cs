using Microsoft.Extensions.Options;
using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem.Services;

public class DbDumpRequestService(IOptions<GmlToFileSystemSettings> options) : IDbDumpRequestService
{
    private readonly GmlToFileSystemSettings _settings = options.Value;

    public DbDumpRequest CreateDbDumpRequest(string? dumpFileName, string? sourceFilePath, string? targetFilePath)
    {
        return new DbDumpRequest
        {
            DbDumpFileLocationPath = _settings.DumpFileLocation,
            DbDumpFileName = dumpFileName ?? string.Empty,
            SourceFilePath = sourceFilePath ?? _settings.AudioSourceLocation,
            TargetFilePath = targetFilePath ?? _settings.AudioTargetLocation
        };
    }
}
