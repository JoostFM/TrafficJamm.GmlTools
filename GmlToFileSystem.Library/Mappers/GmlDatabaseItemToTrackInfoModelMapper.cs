using GmlToFileSystem.Library.Mappers.Helpers;
using GmlToFileSystem.Library.Models;

namespace GmlToFileSystem.Library.Mappers;

public static class GmlDatabaseItemToTrackInfoModelMapper
{
    public static TrackInfoModel Map(GMLDatabaseItem item)
    {
        var reference = item.ItemInfo.ItemId.ToString("X8");
        var trackInfo = new TrackInfoModel
        {
            Artist = item.ItemInfo.ItemInterpret,
            Title = item.ItemInfo.ItemTitle,
            CategoryPath = ParserHelpers.ParseGmlPath(item.CategoryInfo.CategoryNamePath),
            Filename = ParserHelpers.ParseFileName(item.ItemInfo.ItemInterpret, item.ItemInfo.ItemTitle),
            DurationMs = (int)item.ItemInfo.ItemDuration,
            DateAdded = ParserHelpers.ParseDateTimeOffset(item.ItemFiles.FileAudio.FileTimeStampCreated),
            DateModified = ParserHelpers.ParseDateTimeOffset(item.ItemFiles.FileAudio.FileTimeStampModified),
            DateAccessed = ParserHelpers.ParseDateTimeOffset(item.ItemFiles.FileAudio.FileTimeStampAccessed),
            Cues = GetCues(item.ItemMix),
            Metadata = new Dictionary<string, string> {
                        { "Ref", item.ItemReferences.RefReference },
                        { "ItemId", item.ItemInfo.ItemId.ToString() },
                        { "GmlFilename", reference + ".wav" }
                    }
        };
        return trackInfo;
    }
    private static CueSettings GetCues(GMLDatabaseItemItemMix itemMix)
        => new()
        {
            MixTimeFadeIn = (int)itemMix.MixTimeFadeIn,
            MixTimeFadeOut = (int)itemMix.MixTimeFadeOut,
            MixPosBegin = (int)itemMix.MixPosBegin,
            MixPosEnd = (int)itemMix.MixPosEnd,
            MixPosNext = (int)itemMix.MixPosNext,
            MixPosIntro = (int)itemMix.MixPosIntro,
            MixPosOutro = (int)itemMix.MixPosOutro
        };
}
