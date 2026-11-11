using GmlToFileSystem.Library.Gml;
using GmlToFileSystem.Library.Gml.Services;
using GmlToFileSystem.Library.Gml.Workers;
using Microsoft.Extensions.DependencyInjection;

namespace GmlToFileSystem.Library;

public static class GmlApiCollections
{
    public static IServiceCollection AddGmlApiServices(this IServiceCollection services)
    {
        services.AddTransient<IGmlDumpFilePathWorker, GmlDumpFilePathWorker>();
        services.AddTransient<IGmlItemReader, GmlItemReader>();
        services.AddTransient<IGmlDumpFileService, GmlDumpFileService>();
        services.AddTransient<IGmlApi, GmlApi>();

        return services;
    }
}
