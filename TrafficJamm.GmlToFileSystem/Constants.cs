using System.Text.Json;

namespace TrafficJamm.GmlToFileSystem;

internal static class Constants
{
    internal const string OutputFilePath = "D:\\JammFM\\GML\\GmlTrackIndex";

    internal static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
