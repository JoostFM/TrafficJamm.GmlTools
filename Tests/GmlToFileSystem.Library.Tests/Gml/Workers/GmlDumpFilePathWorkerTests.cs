using GmlToFileSystem.Library.Gml.Workers;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.IO.Abstractions;

namespace GmlToFileSystem.Library.Tests.Gml.Workers;

public class GmlDumpFilePathWorkerTests
{
    private readonly IFileSystem _fileSystem = Substitute.For<IFileSystem>();
    private readonly GmlDumpFilePathWorker _worker;

    public GmlDumpFilePathWorkerTests()
    {
        var logger = Substitute.For<ILogger<GmlDumpFilePathWorker>>();
        _worker = new GmlDumpFilePathWorker(_fileSystem, logger);
    }
    [Fact]
    public void GetDbDumpFilePath_WithNullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        string gmlDumpFilePath = null;

        // Act & Assert
        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _worker.GetDbDumpFilePathAsync(gmlDumpFilePath));
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task GetDbDumpFilePathAsync_ShouldReturnExpectedPath()
    {
        // Arrange
        var gmlDumpFilePath = "path/to/db/dump/";
        var expectedDbDumpFilePath = "path/to/db/dump/file.xml";

        _fileSystem.Directory.Exists(gmlDumpFilePath).Returns(true);
        _fileSystem.Directory.GetFiles(gmlDumpFilePath, "*.xml").Returns([expectedDbDumpFilePath]);

        // Act
        var result = await _worker.GetDbDumpFilePathAsync(gmlDumpFilePath);

        // Assert
        Assert.Equal(expectedDbDumpFilePath, result);
    }

    [Fact]
    public async Task GetDbDumpFilePathAsync_WithNoFiles_ShouldThrowFileNotFoundException()
    {
        // Arrange
        var gmlDumpFilePath = "path/to/db/dump/";

        _fileSystem.Directory.Exists(gmlDumpFilePath).Returns(true);
        _fileSystem.Directory.GetFiles(gmlDumpFilePath, "*.xml").Returns([]);

        // Act & Assert
        await Assert.ThrowsAsync<FileNotFoundException>(async () => await _worker.GetDbDumpFilePathAsync(gmlDumpFilePath));
    }

    [Fact]
    public async Task GetDbDumpFilePathAsync_WithNonExistentDirectory_ShouldThrowDirectoryNotFoundException()
    {
        // Arrange
        var gmlDumpFilePath = "path/to/nonexistent/directory/";
        _fileSystem.Directory.Exists(gmlDumpFilePath).Returns(false);
        // Act & Assert
        await Assert.ThrowsAsync<DirectoryNotFoundException>(async () => await _worker.GetDbDumpFilePathAsync(gmlDumpFilePath));
    }
}
