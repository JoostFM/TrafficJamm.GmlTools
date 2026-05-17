using Microsoft.Extensions.DependencyInjection;

namespace TrafficJamm.GmlToFileSystem.Gml;

public static class GmlServiceCollection
{
    public static IServiceCollection AddGmlServices(this IServiceCollection services)
    {
        services.AddTransient<IDbDumpFileService, DbDumpFileService>();
        return services;
    }
}
