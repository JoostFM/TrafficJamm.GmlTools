using System.Text.Json;

namespace TrafficJamm.GmlToFileSystem;

internal static class Constants
{
    internal const string OutputFilePath = "D:\\JammFM\\GML\\GmlTrackIndex";

    internal const string FileSystemTrackListFile = "fileSystemTrackList.json";

    internal static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
