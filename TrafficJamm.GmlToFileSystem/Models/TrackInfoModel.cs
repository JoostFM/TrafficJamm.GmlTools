namespace TrafficJamm.GmlToFileSystem.Models;

public class TrackInfoModel
{
    public string Artist { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryPath { get; set; } = string.Empty;
    public string Filename { get; internal set; } = string.Empty;
    public int DurationMs { get; set; }
    public int MixTimeFadeIn { get; set; }
    public int MixTimeFadeOut { get; set; }
    public int MixPosBegin { get; set; }
    public int MixPosEnd { get; set; }
    public int MixPosNext { get; set; }
    public int MixPosIntro { get; set; }
    public int MixPosOutro { get; set; }
}
