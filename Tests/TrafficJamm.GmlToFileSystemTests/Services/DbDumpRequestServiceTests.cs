using Microsoft.Extensions.Options;
using TrafficJamm.GmlToFileSystem.Models;
using TrafficJamm.GmlToFileSystem.Services;

namespace TrafficJamm.GmlToFileSystem.Tests.Services;

public class DbDumpRequestServiceTests
{
    private readonly DbDumpRequestService _service = new(Options.Create(new GmlToFileSystemSettings
    {
        DumpFileLocation = "/configured/dumps",
        AudioSourceLocation = "/configured/source",
        AudioTargetLocation = "/configured/target"
    }));

    [Fact]
    public void CreateDbDumpRequest_WithNoOptions_UsesConfiguredValues()
    {
        var result = _service.CreateDbDumpRequest(null, null, null);

        Assert.Equal("/configured/dumps", result.DbDumpFileLocationPath);
        Assert.Empty(result.DbDumpFileName);
        Assert.Equal("/configured/source", result.SourceFilePath);
        Assert.Equal("/configured/target", result.TargetFilePath);
    }

    [Fact]
    public void CreateDbDumpRequest_WithOptions_UsesProvidedValues()
    {
        var result = _service.CreateDbDumpRequest("dump.xml", "/custom/source", "/custom/target");

        Assert.Equal("dump.xml", result.DbDumpFileName);
        Assert.Equal("/custom/source", result.SourceFilePath);
        Assert.Equal("/custom/target", result.TargetFilePath);
    }

    [Fact]
    public void CreateDbDumpRequest_WithFullDumpPath_PreservesFullPath()
    {
        const string dumpPath = "/custom/dumps/dump.xml";

        var result = _service.CreateDbDumpRequest(dumpPath, null, null);

        Assert.Equal(dumpPath, result.DbDumpFileName);
    }
}