using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace TrafficJamm.GmlToFileSystem.Workers;

public class DumpFolderWorker(IFileSystem fileSystem) : IDumpFolderWorker
{
    private const string XmlExtension = ".xml";

    public string GetLatestFileFromFolder(string folderPath)
    {
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            throw new ArgumentException("Folder path cannot be null or empty.", nameof(folderPath));
        }

        try
        {
            var files = fileSystem.Directory.GetFiles(folderPath, $"*{XmlExtension}");
            if (files.Length == 0)
            {
                return null;
            }

            var latestFile = files
                .Where(f => IsValidXmlFileName(f))
                .OrderByDescending(f => ExtractDatePrefix(f))
                .FirstOrDefault();

            return latestFile;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to get latest file from folder.", ex);
        }
    }

    private static bool IsValidXmlFileName(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        // Check if filename starts with yyyyMMdd pattern (8 digits)
        return fileName.Length >= 8 && char.IsDigit(fileName[0]) && char.IsDigit(fileName[7]);
    }

    private static string ExtractDatePrefix(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        // Extract the first 8 characters (yyyyMMdd)
        return fileName.Length >= 8 ? fileName.Substring(0, 8) : string.Empty;
    }
}
