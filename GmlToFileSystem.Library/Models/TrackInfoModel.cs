using System.Text.Json.Serialization;

namespace GmlToFileSystem.Library.Models;

public class TrackInfoModel
{
    public string Artist { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;    
    public string CategoryPath { get; set; } = string.Empty;
    public string Filename { get;  set; } = string.Empty;
    public int DurationMs { get; set; }
    public CueSettings Cues { get; set; } = new CueSettings();
    public DateTimeOffset DateAdded { get; set; }

    /// <summary>
    /// Last time the audiofile is physically modified
    /// </summary>
    public DateTimeOffset DateModified { get; set; }
    public DateTimeOffset DateAccessed { get; set; }

    public Dictionary<string, string> Metadata { get; set; } = [];

    /// <summary>
    /// a unique key based on path and filename
    /// </summary>
    [JsonIgnore]
    public string Reference => Path.Combine(CategoryPath, Filename);
}

public class CueSettings
{
    public int MixTimeFadeIn { get; set; }
    public int MixTimeFadeOut { get; set; }
    public int MixPosBegin { get; set; }
    public int MixPosEnd { get; set; }
    public int MixPosNext { get; set; }
    public int MixPosIntro { get; set; }
    public int MixPosOutro { get; set; }
}
