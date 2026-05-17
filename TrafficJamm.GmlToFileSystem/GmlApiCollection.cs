using Microsoft.Extensions.DependencyInjection;
using TrafficJamm.GmlToFileSystem.Gml;

namespace TrafficJamm.GmlToFileSystem;

public static class GmlApiCollection
{
    public static IServiceCollection AddGmlApiServices(this IServiceCollection services)
    {
        services.AddTransient<IGmlApi, GmlApi>();
        services.AddGmlServices();

        return services;
    }
}
