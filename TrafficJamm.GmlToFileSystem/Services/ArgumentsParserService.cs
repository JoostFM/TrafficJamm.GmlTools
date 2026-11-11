using System.CommandLine;
using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Workers;

namespace TrafficJamm.GmlToFileSystem.Services;

public class ArgumentsParserService(IDumpFolderWorker dumpFolderWorker) : IArgumentsParserService
{
    public DbDumpRequest ProcesCliArguments(string[] args)
    {
        ArgumentNullException.ThrowIfNull(args);

        var dumpFileFolderOption = new Option<string>("--dumpfilefolder")
        {
            Description = "The XML dump folder."
        };
        var sourceOption = new Option<string>("--source")
        {
            Description = "The source directory containing GML audio item files."
        };
        var targetOption = new Option<string>("--target")
        {
            Description = "The target directory for the generated track index and audio files."
        };

        var convertCommand = new Command("convert");
        convertCommand.Options.Add(dumpFileFolderOption);
        convertCommand.Options.Add(sourceOption);
        convertCommand.Options.Add(targetOption);

        var rootCommand = new RootCommand("TrafficJamm GML tools");
        rootCommand.Subcommands.Add(convertCommand);

        var parseResult = rootCommand.Parse(args);
        if (parseResult.Errors.Count > 0)
        {
            throw new ArgumentException(string.Join(Environment.NewLine, parseResult.Errors));
        }
        var dumpFileFolder = parseResult.GetValue(dumpFileFolderOption) ?? throw new ArgumentException("Dump file folder is required");

        return new DbDumpRequest
        {            
            DbDumpFileLocationPath = dumpFolderWorker.GetLatestFileFromFolder(dumpFileFolder),
            SourceFilePath = parseResult.GetValue(sourceOption) ?? throw new ArgumentException("Source path is required"),
            TargetFilePath = parseResult.GetValue(targetOption) ?? throw new ArgumentException("Target path is required")
        };
    }

   
}
