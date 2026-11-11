using NSubstitute;
using TrafficJamm.GmlToFileSystem.Services;
using TrafficJamm.GmlToFileSystem.Workers;

namespace TrafficJamm.GmlToFileSystem.Tests.Services;

public class ArgumentsParserServiceTests
{
    private readonly IDumpFolderWorker _mockDumpFolderWorker = Substitute.For<IDumpFolderWorker>();
    private readonly ArgumentsParserService _service;

    public ArgumentsParserServiceTests()
    {
        _mockDumpFolderWorker.GetLatestFileFromFolder(Arg.Any<string>())
            .Returns("/configured/dumps/latest.xml");
        _service = new(_mockDumpFolderWorker);
    }

    [Fact]
    public void CreateDbDumpRequest_WithNoOptions_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.ProcesCliArguments(["convert"]));
        Assert.Contains("required", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CreateDbDumpRequest_WithRequiredOptions_ParsesCommandLineArguments()
    {
        var result = _service.ProcesCliArguments([
            "convert",
            "--dumpfilefolder", "/custom/dumps",
            "--source", "/custom/source",
            "--target", "/custom/target"
        ]);

        Assert.Equal("/configured/dumps/latest.xml", result.DbDumpFileLocationPath);
        Assert.Equal("/custom/source", result.SourceFilePath);
        Assert.Equal("/custom/target", result.TargetFilePath);
    }

    [Fact]
    public void CreateDbDumpRequest_CallsDumpFolderWorkerWithProvidedFolder()
    {
        const string dumpFolder = "/custom/dumps";

        _service.ProcesCliArguments([
            "convert",
            "--dumpfilefolder", dumpFolder,
            "--source", "/source",
            "--target", "/target"
        ]);

        _mockDumpFolderWorker.Received(1).GetLatestFileFromFolder(dumpFolder);
    }
}