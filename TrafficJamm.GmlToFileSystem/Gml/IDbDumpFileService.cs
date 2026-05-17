using TrafficJamm.GmlToFileSystem.Gml.Models;
using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem.Gml;

public interface IDbDumpFileService
{
    List<TrackInfoModel> ConvertToTrackInfoList(List<GMLDatabaseItem> gmlAudioItems);
    string ExportToGmlTrackIndex(List<TrackInfoModel> trackList);
    List<GMLDatabaseItem> FilterAudioItems(List<GMLDatabaseItem> gmlItems);

    /// <summary>
    /// Gets the full path to the database dump file based on the provided request.
    /// </summary>
    /// <param name="request">The request containing the database dump file location and name.</param>
    /// <returns>The full path to the database dump file.</returns>
    string GetDbDumpFilePath(DbDumpRequest request);

    /// <summary>
    /// Parses a database dump file and returns a list of GML database items contained within the file.
    /// </summary>
    /// <param name="dumpFilePath">The full path to the database dump file to parse. Cannot be null or empty.</param>
    /// <returns>A list of GMLDatabaseItem objects</returns>
    List<GMLDatabaseItem> GetGmlItemsFromDbDumpFile(string dumpFilePath);
}