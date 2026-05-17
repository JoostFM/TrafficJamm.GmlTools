using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO.Abstractions;
using TrafficJamm.GmlToFileSystem;
using TrafficJamm.GmlToFileSystem.Gml.Models;

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
        services.AddGmlApiServices();
    })
    .UseSerilog((context, loggerConfig) =>
    {
        loggerConfig
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/gml-conversion-.txt", rollingInterval: RollingInterval.Day)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", "TrafficJamm.GmlToFileSystem");
    })
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

try
{
    logger.LogInformation("Starting GML conversion process...");

    var gmlApi = host.Services.GetRequiredService<IGmlApi>();

    var request = new DbDumpRequest
    {
        DbDumpFileLocationPath = @"D:\JammFM\GML\Dump\Xml",
        DbDumpFileName = "20260515.GML.DbDump.xml",
        TargetFilePath = @"D:\JammFM\GML\GmlTrackIndex"
    };

    logger.LogInformation("Processing XML dump from: {Path}", request.DbDumpFileLocationPath);
    logger.LogInformation("Target output directory: {Target}", request.TargetFilePath);

    var result = await gmlApi.CreateTrackListFromXmlDbDumpGmlAsync(request);

    if (result.Success)
    {
        logger.LogInformation("Conversion completed successfully. Output: {Output}", result.Data);
        logger.LogInformation("Message: {Message}", result.Message);
    }
    else
    {
        logger.LogError("Conversion failed. Message: {Message}", result.Message);
        Environment.Exit(1);
    }
}
catch (Exception ex)
{
    logger.LogError(ex, "An unexpected error occurred during GML conversion");
    Environment.Exit(1);
}
finally
{
    logger.LogInformation("GML conversion process completed");
    //await host.RunAsync();
}
