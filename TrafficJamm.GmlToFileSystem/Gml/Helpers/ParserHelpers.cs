using System.Globalization;
using TrafficJamm.GmlToFileSystem.Utils;

namespace TrafficJamm.GmlToFileSystem.Gml.Helpers;

internal static class ParserHelpers
{
    public static string ParseGmlPath(string categoryNamePath)
    {
        const string RootPartPrefix = "\\!\\";
        const string DjFolderPrefix = "\\- ";
        const string JingleFolderPrefix = "\\! ";

        // Remove the root part prefix if present
        var path = categoryNamePath.StartsWith(RootPartPrefix)
            ? categoryNamePath.Substring(RootPartPrefix.Length)
            : categoryNamePath;

        string returnValue;

        if (path.StartsWith(DjFolderPrefix.TrimStart('\\')))
        {
            // Strip the leading prefix, replace with \Shows\
            returnValue = "\\Shows\\" + path.Substring(DjFolderPrefix.TrimStart('\\').Length);
        }
        else if (path.StartsWith(JingleFolderPrefix.TrimStart('\\')))
        {
            // Strip the leading prefix, replace with \Jingles\
            returnValue = "\\Jingles\\" + path.Substring(JingleFolderPrefix.TrimStart('\\').Length);
        }
        else
        {
            returnValue = "\\music\\" + path.TrimStart('\\');
        }

        return returnValue.GetSafeFilePath();
    }

    public static string ParseFileName(string artist, string title)
    {
        var combined = $"{artist} - {title}.wav".Trim();
        return combined.GetSafeFileName();
    }

    public static DateTimeOffset ParseDateTimeOffset(string raw) =>
        DateTimeOffset.ParseExact(raw, "yyyyMMddHHmmssfff",
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal);

    public static DateTimeOffset ParseDateTimeOffset(ulong raw) =>
        ParseDateTimeOffset(raw.ToString());
}
