using System.Net;
using System.Text;

namespace TrafficJamm.GmlToFileSystem.Utils;

internal static class SafeFileNameUtil
{
    public static string GetSafeFilePath(this string input)
    {
        var parts = input.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

        var returnPaths = new List<string>();

        foreach (var part in parts)
        {
            var safeFilePath = part.GetSafeFileName()
                .Replace("--", "-");
            while (safeFilePath.Contains("--"))
                safeFilePath = safeFilePath.Replace("--", "-");
            returnPaths.Add(safeFilePath);
        }       

        return string.Join(Path.DirectorySeparatorChar, returnPaths);
    }

    public static string GetSafeFileName(this string input)
    {
        var decoded = WebUtility.HtmlDecode(input.Trim());
                
        var safeFileName = new StringBuilder(decoded);

        foreach (var invalidChar in Path.GetInvalidFileNameChars())
        {
            safeFileName.Replace(invalidChar, '_');
        }

        // Optionally, you can also trim the file name to a certain length
        int maxLength = 255; // Maximum file name length for most file systems
        if (safeFileName.Length > maxLength)
        {
            safeFileName.Length = maxLength;
        }

        return safeFileName.ToString();
    }
}
