using GmlToFileSystem.Library.Gml.Models;

namespace GmlToFileSystem.Library.Gml.Workers;

public interface IGmlItemReader
{
    /// <summary>
    /// Parses a GML dump file and returns a list of GML database items contained within the file, 
    /// filtered to include only audio items.
    /// </summary>
    /// <param name="gmlFilePath">The full path to the GML file to parse. Cannot be null or empty.</param>
    /// <returns>A list of GMLDatabaseItem objects</returns>
    Task<List<GMLDatabaseItem>> ReadGmlAudioItemsAsync(string gmlFilePath);
}