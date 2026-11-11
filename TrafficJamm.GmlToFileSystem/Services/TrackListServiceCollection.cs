using Microsoft.Extensions.DependencyInjection;
using TrafficJamm.GmlToFileSystem.Workers;

namespace TrafficJamm.GmlToFileSystem.Services;

public static class TrackListServiceCollection
{
    public static IServiceCollection AddTrackListServices(this IServiceCollection services)
    {
        services.AddTransient<ITrackListService, TrackListService>();
        services.AddTransient<ITrackListFilterWorker, TrackListFilterWorker>();
        return services;
    }
}
