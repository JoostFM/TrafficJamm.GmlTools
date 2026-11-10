using Microsoft.Extensions.DependencyInjection;
using TrafficJamm.GmlToFileSystem.Gml;
using TrafficJamm.GmlToFileSystem.Services;
using TrafficJamm.GmlToFileSystem.Workers;

namespace TrafficJamm.GmlToFileSystem;

public static class GmlApiCollection
{
    public static IServiceCollection AddGmlToFileSystemServices(this IServiceCollection services)
    {
        services.AddTransient<IGmlApi, GmlApi>();
        services.AddTransient<ITrackListApi, TrackListApi>();
        services.AddTransient<IDbDumpRequestService, DbDumpRequestService>();
        services.AddTransient<IDumpFolderWorker, DumpFolderWorker>();
        services.AddGmlServices();
        services.AddTrackListServices();

        return services;
    }
}
