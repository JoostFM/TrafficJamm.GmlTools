using GmlToFileSystem.Library.Gml.Models;

namespace GmlToFileSystem.Library.Gml.Utils;

public static class GmlDatabaseItemListUtils
{
    private const int ItemTypeAudio = 10;
    public static List<GMLDatabaseItem> FilterAudioItems( this GMLDatabaseItem[] gmlItems)
    {
        return [.. gmlItems
        .Where(item => item.ItemInfo != null
            && item.ItemInfo.ItemType == ItemTypeAudio
            && item.ItemFiles.FileAudio.FilePathId == 1
            && item.ItemFiles.FileAudio.FileAttrText != "[Invalid]")];
    }
}
