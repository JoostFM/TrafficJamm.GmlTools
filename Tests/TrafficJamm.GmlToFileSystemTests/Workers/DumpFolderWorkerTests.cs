using NSubstitute;
using System.IO.Abstractions;
using TrafficJamm.GmlToFileSystem.Workers;

namespace TrafficJamm.GmlToFileSystem.Tests.Workers;

public class DumpFolderWorkerTests
{
    private readonly IFileSystem _fileSystem = Substitute.For<IFileSystem>();
    private readonly DumpFolderWorker _worker;

    public DumpFolderWorkerTests()
    {
        _worker = new DumpFolderWorker(_fileSystem);
    }

    #region GetLatestFileFromFolder - Happy Path Tests

    [Fact]
    public void GetLatestFileFromFolder_WithValidFolderAndSingleXmlFile_ReturnsTheFile()
    {
        // Arrange
        const string folderPath = "/test/folder";
        const string filePath = "/test/folder/20250115dump.xml";
        _fileSystem.Directory.GetFiles(folderPath, "*.xml").Returns([filePath]);

        // Act
        var result = _worker.GetLatestFileFromFolder(folderPath);

        // Assert
        Assert.Equal(filePath, result);
    }

    [Fact]
    public void GetLatestFileFromFolder_WithMultipleXmlFiles_ReturnsLatestByDatePrefix()
    {
        // Arrange
        const string folderPath = "/test/folder";
        var files = new[]
        {
            "/test/folder/20250101dump.xml",
            "/test/folder/20250115dump.xml",
            "/test/folder/20250110dump.xml"
        };
        _fileSystem.Directory.GetFiles(folderPath, "*.xml").Returns(files);

        // Act
        var result = _worker.GetLatestFileFromFolder(folderPath);

        // Assert
        Assert.Equal("/test/folder/20250115dump.xml", result);
    }

    [Fact]
    public void GetLatestFileFromFolder_WithMultipleXmlFilesOutOfOrder_ReturnsLatestByDatePrefix()
    {
        // Arrange
        const string folderPath = "/test/folder";
        var files = new[]
        {
            "/test/folder/20250115dump.xml",
            "/test/folder/20250101dump.xml",
            "/test/folder/20250120dump.xml"
        };
        _fileSystem.Directory.GetFiles(folderPath, "*.xml").Returns(files);

        // Act
        var result = _worker.GetLatestFileFromFolder(folderPath);

        // Assert
        Assert.Equal("/test/folder/20250120dump.xml", result);
    }

    [Fact]
    public void GetLatestFileFromFolder_WithFilesHavingDifferentNameFormats_ReturnsLatestValidFile()
    {
        // Arrange
        const string folderPath = "/test/folder";
        var files = new[]
        {
            "/test/folder/20250115dump.xml",
            "/test/folder/invalidname.xml",
            "/test/folder/20250120data.xml"
        };
        _fileSystem.Directory.GetFiles(folderPath, "*.xml").Returns(files);

        // Act
        var result = _worker.GetLatestFileFromFolder(folderPath);

        // Assert
        Assert.Equal("/test/folder/20250120data.xml", result);
    }

    [Fact]
    public void GetLatestFileFromFolder_WithAllFilesHavingInvalidFormat_ReturnsNull()
    {
        // Arrange
        const string folderPath = "/test/folder";
        var files = new[]
        {
            "/test/folder/invalidname.xml",
            "/test/folder/nodate.xml"
        };
        _fileSystem.Directory.GetFiles(folderPath, "*.xml").Returns(files);

        // Act
        var result = _worker.GetLatestFileFromFolder(folderPath);

        // Assert
        Assert.Null(result);
    }

    [Theory]
    [InlineData("/test/folder/20250101dump.xml")]
    [InlineData("/test/folder/20251231data.xml")]
    [InlineData("/test/folder/20200101extract.xml")]
    public void GetLatestFileFromFolder_WithValidDateFormats_ReturnsFile(string filePath)
    {
        // Arrange
        const string folderPath = "/test/folder";
        _fileSystem.Directory.GetFiles(folderPath, "*.xml").Returns([filePath]);

        // Act
        var result = _worker.GetLatestFileFromFolder(folderPath);

        // Assert
        Assert.Equal(filePath, result);
    }

    #endregion

    #region GetLatestFileFromFolder - Empty Folder Tests

    [Fact]
    public void GetLatestFileFromFolder_WithEmptyFolder_ReturnsNull()
    {
        // Arrange
        const string folderPath = "/test/empty";
        _fileSystem.Directory.GetFiles(folderPath, "*.xml").Returns([]);

        // Act
        var result = _worker.GetLatestFileFromFolder(folderPath);

        // Assert
        Assert.Null(result);
    }

    #endregion

    #region GetLatestFileFromFolder - Null/Empty Path Tests

    [Fact]
    public void GetLatestFileFromFolder_WithNullFolderPath_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _worker.GetLatestFileFromFolder(null!));
        Assert.Equal("folderPath", ex.ParamName);
        Assert.Contains("Folder path cannot be null or empty", ex.Message);
    }

    [Fact]
    public void GetLatestFileFromFolder_WithEmptyFolderPath_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _worker.GetLatestFileFromFolder(""));
        Assert.Equal("folderPath", ex.ParamName);
        Assert.Contains("Folder path cannot be null or empty", ex.Message);
    }

    [Fact]
    public void GetLatestFileFromFolder_WithWhitespaceFolderPath_ThrowsArgumentException()
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => _worker.GetLatestFileFromFolder("   "));
        Assert.Equal("folderPath", ex.ParamName);
        Assert.Contains("Folder path cannot be null or empty", ex.Message);
    }

    #endregion

    #region GetLatestFileFromFolder - Exception Handling Tests

    [Fact]
    public void GetLatestFileFromFolder_WhenDirectoryThrowsIOException_ThrowsInvalidOperationException()
    {
        // Arrange
        const string folderPath = "/test/folder";
        _fileSystem.Directory.When(x => x.GetFiles(folderPath, "*.xml")).Throw<IOException>();

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => _worker.GetLatestFileFromFolder(folderPath));
        Assert.Equal("Failed to get latest file from folder.", ex.Message);
        Assert.IsType<IOException>(ex.InnerException);
    }

    [Fact]
    public void GetLatestFileFromFolder_WhenDirectoryThrowsUnauthorizedAccessException_ThrowsInvalidOperationException()
    {
        // Arrange
        const string folderPath = "/test/protected";
        _fileSystem.Directory.When(x => x.GetFiles(folderPath, "*.xml")).Throw<UnauthorizedAccessException>();

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => _worker.GetLatestFileFromFolder(folderPath));
        Assert.Equal("Failed to get latest file from folder.", ex.Message);
        Assert.IsType<UnauthorizedAccessException>(ex.InnerException);
    }

    [Fact]
    public void GetLatestFileFromFolder_WhenDirectoryThrowsDirectoryNotFoundException_ThrowsInvalidOperationException()
    {
        // Arrange
        const string folderPath = "/test/nonexistent";
        _fileSystem.Directory.When(x => x.GetFiles(folderPath, "*.xml")).Throw<DirectoryNotFoundException>();

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => _worker.GetLatestFileFromFolder(folderPath));
        Assert.Equal("Failed to get latest file from folder.", ex.Message);
        Assert.IsType<DirectoryNotFoundException>(ex.InnerException);
    }

    #endregion
}
