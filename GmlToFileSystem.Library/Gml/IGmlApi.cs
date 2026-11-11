using GmlToFileSystem.Library.Models;

namespace GmlToFileSystem.Library.Gml;

public interface IGmlApi
{
    /// <summary>
    /// Read GmlDumpFile and export as Tracklist
    /// </summary>
    /// <param name="request"></param>
    /// <returns>List of track info models</returns>
    Task<List<TrackInfoModel>> GetTrackListFromXmlDbDumpGmlAsync(string gmlFilePath);
}