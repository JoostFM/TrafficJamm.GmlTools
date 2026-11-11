using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO.Abstractions;
using System.Reflection;
using TrafficJamm.GmlToFileSystem;
using TrafficJamm.GmlToFileSystem.Process;

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
        services.AddGmlToFileSystemServices();        
    })
    .UseSerilog((context, loggerConfig) => loggerConfig
        .ReadFrom.Configuration(context.Configuration))
    .Build();

var logger = host.Services.GetRequiredService<ILogger<Program>>();

try
{
    var version = Assembly.GetExecutingAssembly().GetName().Version;
    logger.LogInformation("Starting GML conversion process... Version: {Version}", version);

    var process = host.Services.GetRequiredService<IGmlToFileSystemProcessor>();

    var result = await process.ProcessAsync(args);
        
    if (!result.Success)
    {
        logger.LogError("Conversion failed. Message: {Message}", result.Message);
        return 1;
    }

    logger.LogInformation("Conversion completed successfully. {Message}", result.Message);
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
