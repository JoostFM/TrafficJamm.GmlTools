namespace TrafficJamm.GmlToFileSystem.Gml.Models;

public class DbDumpRequest
{
    /// <summary>
    /// Location for the Gml Dmup Xml file
    /// </summary>
    public required string DbDumpFileLocationPath { get; set; } = string.Empty;
    public required string SourceFilePath { get; set; } = string.Empty;
    public required string TargetFilePath { get; set; } = string.Empty;
}
