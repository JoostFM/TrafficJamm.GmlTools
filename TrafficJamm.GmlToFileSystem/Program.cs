using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System.IO.Abstractions;
using System.CommandLine;
using TrafficJamm.GmlToFileSystem;
using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Models;
using TrafficJamm.GmlToFileSystem.Services;

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

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<IFileSystem, FileSystem>();
        services.Configure<GmlToFileSystemSettings>(context.Configuration.GetSection("GmlToFileSystemSettings"));
        services.AddGmlToFileSystemServices();
    })
    .UseSerilog((context, loggerConfig) => loggerConfig
        .ReadFrom.Configuration(context.Configuration))
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
var dbDumpRequestService = host.Services.GetRequiredService<IDbDumpRequestService>();

var convertCommand = new Command("convert", "Convert a GML database dump to the file system.");
convertCommand.Options.Add(filenameOption);
convertCommand.Options.Add(sourceOption);
convertCommand.Options.Add(targetOption);
convertCommand.SetAction(async parseResult =>
{
    try
    {
        logger.LogInformation("Starting GML conversion process...");

        var gmlApi = host.Services.GetRequiredService<IGmlApi>();
        var trackListApi = host.Services.GetRequiredService<ITrackListApi>();
        var request = dbDumpRequestService.CreateDbDumpRequest(
            parseResult.GetValue(filenameOption),
            parseResult.GetValue(sourceOption),
            parseResult.GetValue(targetOption));

        logger.LogInformation("Processing XML dump from: {Path}", request.DbDumpFileLocationPath);
        logger.LogInformation("Target output directory: {Target}", request.TargetFilePath);

        var result = await gmlApi.CreateTrackListFromXmlDbDumpGmlAsync(request);

        if (!result.Success)
        {
            logger.LogError("Conversion failed. Message: {Message}", result.Message);
            return;
        }

        logger.LogInformation("Conversion completed successfully. Output: {Output}", result.Data);
        logger.LogInformation("Message: {Message}", result.Message);

        var copyResult = await trackListApi.TransferGmlFilesToFileSystem(request, result.Data);
        if (copyResult.Success)
        {
            logger.LogInformation("GML files transferred successfully. Message: {Message}", copyResult.Message);
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An unexpected error occurred during GML conversion");
    }
    finally
    {
        logger.LogInformation("GML conversion process completed");
    }
});

var rootCommand = new RootCommand("TrafficJamm GML tools");
rootCommand.Subcommands.Add(convertCommand);

return await rootCommand.Parse(args).InvokeAsync();
