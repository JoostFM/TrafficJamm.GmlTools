using GmlToFileSystem.Library.Gml.Services;
using GmlToFileSystem.Library.Models;
using Microsoft.Extensions.Logging;

namespace GmlToFileSystem.Library.Gml;

public class GmlApi(IGmlDumpFileService dbDumpFileService,  ILogger<GmlApi> logger) : IGmlApi
{
    public async Task<List<TrackInfoModel>> GetTrackListFromXmlDbDumpGmlAsync(string gmlFilePath)
    {
        var trackInfoList = await dbDumpFileService.ConvertGmlDumpFileToTrackListAsync(gmlFilePath);
        
        logger.LogInformation("Retrieved {Count} audio items from XML dump", trackInfoList.Count);
        
        return trackInfoList;
    }
}