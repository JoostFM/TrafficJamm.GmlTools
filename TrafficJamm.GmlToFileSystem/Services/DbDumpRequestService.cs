using System.CommandLine;
using Microsoft.Extensions.Options;
using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem.Services;

public class DbDumpRequestService(IOptions<GmlToFileSystemSettings> options) : IDbDumpRequestService
{
    private readonly GmlToFileSystemSettings _settings = options.Value;

    public DbDumpRequest CreateDbDumpRequest(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        var filenameOption = new Option<string?>("--filename")
        {
            Description = "The XML dump file name or full path."
        };
        var sourceOption = new Option<string?>("--source")
        {
            Description = "The source directory containing GML audio files."
        };
        var targetOption = new Option<string?>("--target")
        {
            Description = "The target directory for the generated track index and audio files."
        };

        var convertCommand = new Command("convert");
        convertCommand.Options.Add(filenameOption);
        convertCommand.Options.Add(sourceOption);
        convertCommand.Options.Add(targetOption);

        var rootCommand = new RootCommand("TrafficJamm GML tools");
        rootCommand.Subcommands.Add(convertCommand);

        var parseResult = rootCommand.Parse(args);
        if (parseResult.Errors.Count > 0)
        {
            throw new ArgumentException(string.Join(Environment.NewLine, parseResult.Errors));
        }

        return new DbDumpRequest
        {
            DbDumpFileLocationPath = _settings.DumpFileLocation,
            DbDumpFileName = parseResult.GetValue(filenameOption) ?? string.Empty,
            SourceFilePath = parseResult.GetValue(sourceOption) ?? _settings.AudioSourceLocation,
            TargetFilePath = parseResult.GetValue(targetOption) ?? _settings.AudioTargetLocation
        };
    }
}
