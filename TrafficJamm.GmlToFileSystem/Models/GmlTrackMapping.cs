namespace TrafficJamm.GmlToFileSystem.Models;

public class GmlTrackMapping
{
    public string Id { get; set; } = string.Empty;
    public string Reference  => Path.Combine(TrackInfo.CategoryPath, TrackInfo.Filename) ?? string.Empty;
    public TrackInfoModel TrackInfo { get; set; }
}
