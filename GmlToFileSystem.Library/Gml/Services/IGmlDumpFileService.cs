using GmlToFileSystem.Library.Models;

namespace GmlToFileSystem.Library.Gml.Services
{
    public interface IGmlDumpFileService
    {
        Task<List<TrackInfoModel>> ConvertGmlDumpFileToTrackListAsync(string gmlDumpFilePath);
    }
}