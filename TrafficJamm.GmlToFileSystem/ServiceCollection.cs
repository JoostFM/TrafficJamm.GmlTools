using GmlToFileSystem.Library;
using GmlToFileSystem.Library.Gml;
using Microsoft.Extensions.DependencyInjection;
using TrafficJamm.GmlToFileSystem.Gml;
using TrafficJamm.GmlToFileSystem.Process;
using TrafficJamm.GmlToFileSystem.Services;
using TrafficJamm.GmlToFileSystem.Workers;

namespace TrafficJamm.GmlToFileSystem;

public static class GmlToFileSystemSCollection
{
    public static IServiceCollection AddGmlToFileSystemServices(this IServiceCollection services)
    {
        services.AddTransient<ITrackListApi, TrackListApi>();
        services.AddTransient<IArgumentsParserService, ArgumentsParserService>();
        services.AddTransient<IDumpFolderWorker, DumpFolderWorker>();
        
        services.AddTrackListServices();
        services.AddGmlApiServices();
        services.AddTransient<IGmlToFileSystemProcessor, GmlToFileSystemProcessor>();

        return services;
    }
}
