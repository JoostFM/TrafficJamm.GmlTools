using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions;

namespace TrafficJamm.GmlToFileSystem.Gml;

public static class GmlServiceCollection
{
    public static IServiceCollection AddGmlServices(this IServiceCollection services)
    {
        services.AddTransient<IDbDumpFileService, DbDumpFileService>();
        
        return services;
    }
}
