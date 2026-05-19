using Microsoft.Extensions.DependencyInjection;

namespace TrafficJamm.GmlToFileSystem.Services;

public static class TrackListServiceCollection
{
    public static IServiceCollection AddTrackListServices(this IServiceCollection services)
    {
        services.AddTransient<ITrackListService, TrackListService>();
        
        return services;
    }
}
