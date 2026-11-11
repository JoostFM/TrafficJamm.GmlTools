using GmlToFileSystem.Library.Gml;
using GmlToFileSystem.Library.Models;
using Microsoft.Extensions.Logging;
using System.IO.Abstractions;
using System.Text.Json;
using TrafficJamm.GmlToFileSystem.Models;
using TrafficJamm.GmlToFileSystem.Services;

namespace TrafficJamm.GmlToFileSystem.Process;

public class GmlToFileSystemProcessor(IGmlApi gmlApi, ITrackListApi trackListApi, IArgumentsParserService dbDumpRequestService, IFileSystem fileSystem,ILogger<GmlToFileSystemProcessor> logger) : IGmlToFileSystemProcessor
{
    public async Task<Result> ProcessAsync(string[] args)
    {
        var request = dbDumpRequestService.ProcesCliArguments(args);

        var gmlTrackList = await gmlApi.GetTrackListFromXmlDbDumpGmlAsync(request.DbDumpFileLocationPath);

        if(gmlTrackList.Count == 0)
        {
            logger.LogWarning("No audio items found in GML file");
            return new Result
            {
                Success = true,
                Message = $"No audio items found in GML file"
            };
        }

        var fileName = ExportToGmlTrackIndex(gmlTrackList);

        await trackListApi.TransferGmlFilesToFileSystem(request.SourceFilePath, request.TargetFilePath, fileName);

        // Placeholder for the actual processing logic
        // This is where you would read the GML file, parse it, and write the output to the file system

        return new Result
        {
            Success = true,
            Message = "Processing completed successfully."
        };
    }

    private string ExportToGmlTrackIndex(List<TrackInfoModel> trackList)
    {
        var content = JsonSerializer.Serialize(trackList, Constants.JsonOptions);

        var filename = Path.Combine(Constants.OutputFilePath, $"GmlTrackIndex_{DateTime.Now:yyyyMMdd_HHmmss}.json");

        fileSystem.File.WriteAllText(filename, content);

        return filename;
    }
}
