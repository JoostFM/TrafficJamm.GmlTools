using TrafficJamm.GmlToFileSystem.Utils;

namespace TrafficJamm.GmlToFileSystem.Tests.Utils;

public class SafeFileNameUtilTests
{
    #region GetSafeFileName Tests

    [Fact]
    public void GetSafeFileName_WithValidInput_ReturnsUnchanged()
    {
        // Arrange
        const string input = "ValidFileName.txt";

        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.Equal("ValidFileName.txt", result);
    }

    [Theory]
    [InlineData("file<name>.txt", "file_name_.txt")]
    [InlineData("file>name.txt", "file_name.txt")]
    [InlineData("file|name.txt", "file_name.txt")]
    [InlineData("file:name.txt", "file_name.txt")]
    [InlineData("file*name.txt", "file_name.txt")]
    [InlineData("file?name.txt", "file_name.txt")]
    [InlineData("file\"name.txt", "file_name.txt")]
    public void GetSafeFileName_WithInvalidCharacters_ReplacesWithUnderscore(string input, string expected)
    {
        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetSafeFileName_WithHtmlEntities_DecodesCorrectly()
    {
        // Arrange
        const string input = "file&amp;name.txt";

        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.Equal("file&name.txt", result);
    }

    [Fact]
    public void GetSafeFileName_WithLeadingTrailingWhitespace_TrimsWhitespace()
    {
        // Arrange
        const string input = "  filename.txt  ";

        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.Equal("filename.txt", result);
    }

    [Fact]
    public void GetSafeFileName_ExceedingMaxLength_IsTruncatedTo255Characters()
    {
        // Arrange
        var input = new string('a', 300) + ".txt";

        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.Equal(255, result.Length);
    }


    [Fact]
    public void GetSafeFileName_WithMultipleHtmlEntities_DecodesAll()
    {
        // Arrange
        const string input = "file&lt;name&gt;.txt";

        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.Equal("file_name_.txt", result);
    }

    [Fact]
    public void GetSafeFileName_WithOnlyInvalidCharacters_ReplacesAll()
    {
        // Arrange
        const string input = "<>|:*?\"";

        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.Equal("_______", result);
    }

    [Fact]
    public void GetSafeFileName_WithEmptyString_ReturnsEmptyString()
    {
        // Arrange
        const string input = "";

        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.Equal("", result);
    }

    [Fact]
    public void GetSafeFileName_WithOnlyWhitespace_ReturnsEmptyString()
    {
        // Arrange
        const string input = "   ";

        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.Equal("", result);
    }

    #endregion

    #region GetSafeFilePath Tests

    [Fact]
    public void GetSafeFilePath_WithValidPath_ReturnsUnchanged()
    {
        // Arrange
        var input = Path.Combine("folder", "subfolder", "file.txt");

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        Assert.Contains("folder", result);
        Assert.Contains("subfolder", result);
        Assert.Contains("file.txt", result);
    }

    [Fact]
    public void GetSafeFilePath_WithInvalidCharactersInParts_SanitizesEachPart()
    {
        // Arrange
        var input = Path.Combine("fold<er>", "sub*folder", "file?.txt");

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        Assert.DoesNotContain("<", result);
        Assert.DoesNotContain(">", result);
        Assert.DoesNotContain("*", result);
        Assert.DoesNotContain("?", result);
    }

    [Fact]
    public void GetSafeFilePath_WithDoubleDashes_ReducesToSingleDash()
    {
        // Arrange
        var input = Path.Combine("fold--er", "file--name.txt");

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        Assert.DoesNotContain("--", result);
    }

    [Fact]
    public void GetSafeFilePath_WithWindowsPath_PreservesPathStructure()
    {
        // Arrange
        var input = "folder\\subfolder\\file.txt";

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        Assert.Contains("folder", result);
        Assert.Contains("subfolder", result);
        Assert.Contains("file.txt", result);
    }

    [Fact]
    public void GetSafeFilePath_WithUnixPath_PreservesPathStructure()
    {
        // Arrange
        const string input = "folder/subfolder/file.txt";

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        Assert.Contains("folder", result);
        Assert.Contains("subfolder", result);
        Assert.Contains("file.txt", result);
    }

    [Fact]
    public void GetSafeFilePath_WithMixedSeparators_SplitsCorrectly()
    {
        // Arrange
        const string input = "folder\\subfolder/file.txt";

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        // Verify it was split into parts and rejoined
        var parts = result.Split(Path.DirectorySeparatorChar);
        Assert.True(parts.Length >= 3);
    }

    [Fact]
    public void GetSafeFilePath_WithHtmlEntitiesInPath_DecodesAll()
    {
        // Arrange
        var input = Path.Combine("fold&lt;er&gt;", "file&amp;name.txt");

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        Assert.DoesNotContain("&lt;", result);
        Assert.DoesNotContain("&gt;", result);
        Assert.DoesNotContain("&amp;", result);
    }

    [Fact]
    public void GetSafeFilePath_WithEmptyParts_HandlesGracefully()
    {
        // Arrange
        var input = Path.Combine("folder", "", "file.txt");

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void GetSafeFilePath_WithRelativePath_PreservesRelativeStructure()
    {
        // Arrange
        const string input = "./folder/subfolder/file.txt";

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        Assert.Contains("folder", result);
        Assert.Contains("subfolder", result);
    }

    [Fact]
    public void GetSafeFilePath_WithDeepPath_HandlesMultipleLevels()
    {
        // Arrange
        var input = Path.Combine("a", "b", "c", "d", "e", "file.txt");

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        var parts = result.Split(Path.DirectorySeparatorChar);
        Assert.True(parts.Length >= 6);
    }

    #endregion

    #region Edge Cases

    [Fact(Skip = "Not implemented yet")]
    public void GetSafeFileName_WithUnicodeCharacters_PreservesValidUnicode()
    {
        // Arrange
        const string input = "файл_文件.txt"; // Russian and Chinese characters

        // Act
        var result = input.GetSafeFileName();

        // Assert
        Assert.NotEmpty(result);
        Assert.DoesNotContain("\0", result);
    }
        
    [Fact]
    public void GetSafeFilePath_WithVeryLongPathParts_HandlesCorrectly()
    {
        // Arrange
        var longPart = new string('a', 300);
        var input = Path.Combine(longPart, "file.txt");

        // Act
        var result = input.GetSafeFilePath();

        // Assert
        Assert.NotNull(result);
        // Each part should be truncated to 255 chars
        var parts = result.Split(Path.DirectorySeparatorChar);
        Assert.All(parts, part => Assert.True(part.Length <= 255));
    }

    #endregion
}