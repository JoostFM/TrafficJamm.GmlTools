using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO.Abstractions;
using TrafficJamm.GmlToFileSystem;
using TrafficJamm.GmlToFileSystem.Models;
using TrafficJamm.GmlToFileSystem.Services;

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

try
{
    logger.LogInformation("Starting GML conversion process...");

    var gmlApi = host.Services.GetRequiredService<IGmlApi>();
    var trackListApi = host.Services.GetRequiredService<ITrackListApi>();
    var request = dbDumpRequestService.CreateDbDumpRequest(args);

    logger.LogInformation("Processing XML dump from: {Path}", request.DbDumpFileLocationPath);
    logger.LogInformation("Target output directory: {Target}", request.TargetFilePath);

    var result = await gmlApi.CreateTrackListFromXmlDbDumpGmlAsync(request);

    if (!result.Success)
    {
        logger.LogError("Conversion failed. Message: {Message}", result.Message);
        return 1;
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
    return 1;
}
finally
{
    logger.LogInformation("GML conversion process completed");
}

return 0;
