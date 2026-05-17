using TrafficJamm.GmlToFileSystem.Gml;
using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem;

public class GmlApi(IDbDumpFileService dbDumpFileService) : IGmlApi
{
    public async Task<Result<string>> CreateTrackListFromXmlDbDumpGmlAsync(DbDumpRequest request)
    {
        var dumpFilePath = dbDumpFileService.GetDbDumpFilePath(request);
        var gmlItems = dbDumpFileService.GetGmlItemsFromDbDumpFile(dumpFilePath);
        var gmlAudioItems = dbDumpFileService.FilterAudioItems(gmlItems);
        var trackList = dbDumpFileService.ConvertToTrackInfoList(gmlAudioItems);

        var exportToFile = dbDumpFileService.ExportToGmlTrackIndex(trackList);
        
        var result = new Result<string>
        {
            Success = true,
            Message = $"Generated {exportToFile}",
            Data = exportToFile
        };
        await Task.CompletedTask;
        return result;
    }
}