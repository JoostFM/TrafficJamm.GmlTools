using System.IO.Abstractions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TrafficJamm.GmlToFileSystem.Gml;
using TrafficJamm.GmlToFileSystem.Gml.Models;

namespace TrafficJamm.GmlToFileSystemTests.Gml;

public class DbDumpFileServiceTests
{
    private readonly IFileSystem _fileSystem = Substitute.For<IFileSystem>();
    private readonly DbDumpFileService _service;

    public DbDumpFileServiceTests()
    {
        _service = new DbDumpFileService(_fileSystem);
    }

    #region GetDbDumpFilePath - Null/Invalid Input Tests

    [Fact]
    public void GetDbDumpFilePath_WithNullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        DbDumpRequest? request = null;

        // Act & Assert
        var exception = Assert.Throws<ArgumentNullException>(() => _service.GetDbDumpFilePath(request!));
        Assert.Equal("request", exception.ParamName);
    }

    #endregion

    #region GetDbDumpFilePath - Directory Not Found Tests

    [Fact]
    public void GetDbDumpFilePath_WithNonExistentDirectory_ThrowsDirectoryNotFoundException()
    {
        // Arrange
        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = "/non/existent/path",
            DbDumpFileName = "",
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(request.DbDumpFileLocationPath).Returns(false);

        // Act & Assert
        var exception = Assert.Throws<DirectoryNotFoundException>(() => _service.GetDbDumpFilePath(request));
        Assert.Contains("does not exist", exception.Message);
        Assert.Contains(request.DbDumpFileLocationPath, exception.Message);
    }

    [Fact]
    public void GetDbDumpFilePath_WhenDirectoryExistenceCheckFails_PropagatesException()
    {
        // Arrange
        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = "/some/path",
            DbDumpFileName = "",
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(request.DbDumpFileLocationPath)
            .Throws(new UnauthorizedAccessException("Access denied"));

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => _service.GetDbDumpFilePath(request));
    }

    #endregion

    #region GetDbDumpFilePath - With Explicit FileName Tests

    [Fact]
    public void GetDbDumpFilePath_WithExplicitFileName_ReturnsCombinedPath()
    {
        // Arrange
        var directoryPath = "/data/dumps";
        var fileName = "database.xml";
        var expectedPath = Path.Combine(directoryPath, fileName);

        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = fileName,
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);

        // Act
        var result = _service.GetDbDumpFilePath(request);

        // Assert
        Assert.Equal(expectedPath, result);
    }

    [Fact]
    public void GetDbDumpFilePath_WithExplicitFileName_DoesNotCallGetFiles()
    {
        // Arrange
        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = "/data/dumps",
            DbDumpFileName = "specific.xml",
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(request.DbDumpFileLocationPath).Returns(true);

        // Act
        _service.GetDbDumpFilePath(request);

        // Assert
        _fileSystem.Directory.DidNotReceive().GetFiles(Arg.Any<string>(), Arg.Any<string>());
    }

    [Fact]
    public void GetDbDumpFilePath_WithWhitespaceFileName_SearchesForXmlFiles()
    {
        // Arrange
        var directoryPath = "/data/dumps";
        var fileName = "  ";
        var expectedFile = "/data/dumps/whitespace.xml";

        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = fileName,
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);
        _fileSystem.Directory.GetFiles(directoryPath, "*.xml").Returns(new[] { expectedFile });

        // Act
        var result = _service.GetDbDumpFilePath(request);

        // Assert
        Assert.Equal(expectedFile, result);
        _fileSystem.Directory.Received(1).GetFiles(directoryPath, "*.xml");
    }

    #endregion

    #region GetDbDumpFilePath - Auto-Discover File Tests

    [Fact]
    public void GetDbDumpFilePath_WithNoFileName_SearchesForXmlFiles()
    {
        // Arrange
        var directoryPath = "/data/dumps";
        var expectedFile = "/data/dumps/database.xml";

        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = "",
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);
        _fileSystem.Directory.GetFiles(directoryPath, "*.xml").Returns(new[] { expectedFile });

        // Act
        var result = _service.GetDbDumpFilePath(request);

        // Assert
        Assert.Equal(expectedFile, result);
    }

    [Fact]
    public void GetDbDumpFilePath_WithNoFileName_ReturnsFirstXmlFile()
    {
        // Arrange
        var directoryPath = "/data/dumps";
        var files = new[] { "/data/dumps/first.xml", "/data/dumps/second.xml", "/data/dumps/third.xml" };

        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = "",
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);
        _fileSystem.Directory.GetFiles(directoryPath, "*.xml").Returns(files);

        // Act
        var result = _service.GetDbDumpFilePath(request);

        // Assert
        Assert.Equal(files[0], result);
    }

    [Fact]
    public void GetDbDumpFilePath_WithNoFileNameAndNoXmlFiles_ThrowsFileNotFoundException()
    {
        // Arrange
        var directoryPath = "/data/dumps";

        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = "",
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);
        _fileSystem.Directory.GetFiles(directoryPath, "*.xml").Returns(Array.Empty<string>());

        // Act & Assert
        var exception = Assert.Throws<FileNotFoundException>(() => _service.GetDbDumpFilePath(request));
        Assert.Contains("No XML files found", exception.Message);
        Assert.Contains(directoryPath, exception.Message);
    }

    [Fact]
    public void GetDbDumpFilePath_WithNullFileName_SearchesForXmlFiles()
    {
        // Arrange
        var directoryPath = "/data/dumps";
        var expectedFile = "/data/dumps/database.xml";

        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = null!,
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);
        _fileSystem.Directory.GetFiles(directoryPath, "*.xml").Returns(new[] { expectedFile });

        // Act
        var result = _service.GetDbDumpFilePath(request);

        // Assert
        Assert.Equal(expectedFile, result);
    }

    #endregion

    #region GetDbDumpFilePath - Edge Cases

    [Fact]
    public void GetDbDumpFilePath_WithMultipleXmlFiles_ReturnsFirst()
    {
        // Arrange
        var directoryPath = "/data";
        var firstFile = "/data/alpha.xml";
        var secondFile = "/data/beta.xml";

        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = "",
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);
        _fileSystem.Directory.GetFiles(directoryPath, "*.xml").Returns(new[] { firstFile, secondFile });

        // Act
        var result = _service.GetDbDumpFilePath(request);

        // Assert
        Assert.Equal(firstFile, result);
    }

    [Fact]
    public void GetDbDumpFilePath_CallsDirectoryExistsWithCorrectPath()
    {
        // Arrange
        var directoryPath = "/custom/path";
        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = "file.xml",
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);

        // Act
        _service.GetDbDumpFilePath(request);

        // Assert
        _fileSystem.Directory.Received(1).Exists(directoryPath);
    }

    [Fact]
    public void GetDbDumpFilePath_WithEmptyStringFileName_SearchesForXmlFiles()
    {
        // Arrange
        var directoryPath = "/data/dumps";
        var expectedFile = "/data/dumps/result.xml";

        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = string.Empty,
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);
        _fileSystem.Directory.GetFiles(directoryPath, "*.xml").Returns(new[] { expectedFile });

        // Act
        var result = _service.GetDbDumpFilePath(request);

        // Assert
        Assert.Equal(expectedFile, result);
        _fileSystem.Directory.Received(1).GetFiles(directoryPath, "*.xml");
    }

    [Fact]
    public void GetDbDumpFilePath_WhenGetFilesThrowsException_PropagatesException()
    {
        // Arrange
        var directoryPath = "/data";
        var request = new DbDumpRequest
        {
            DbDumpFileLocationPath = directoryPath,
            DbDumpFileName = "",
            TargetFilePath = "/target"
        };

        _fileSystem.Directory.Exists(directoryPath).Returns(true);
        _fileSystem.Directory.GetFiles(directoryPath, "*.xml")
            .Throws(new UnauthorizedAccessException("Cannot access directory"));

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => _service.GetDbDumpFilePath(request));
    }

    #endregion
}