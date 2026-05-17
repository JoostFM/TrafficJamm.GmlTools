namespace TrafficJamm.GmlToFileSystem.Gml.Models;

public class DbDumpRequest
{
    public required string DbDumpFileLocationPath { get; set; } = string.Empty;
    public string DbDumpFileName { get; set; } = string.Empty;
    public required string TargetFilePath { get; set; } = string.Empty;
}
