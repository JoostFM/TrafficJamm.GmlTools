using TrafficJamm.GmlToFileSystem.Gml.Helpers;

namespace TrafficJamm.GmlToFileSystem.Tests.Gml.Helpers;

public class ParseGmlPathTests
{
    // Helper: mirrors GetSafeFilePath as identity so tests are self-contained.
    // If your real implementation sanitises characters, adjust expected values accordingly.

    [Theory]
    [InlineData(
        @"\!\- Freaky Friday\_FF Archive",
        @"\Shows\Freaky Friday\_FF Archive",
        "DJ folder prefix is replaced with \\Shows\\")]
    [InlineData(
        @"\!\! Jamm Slow Sweepers",
        @"\Jingles\Jamm Slow Sweepers",
        "Jingle folder prefix is replaced with \\Jingles\\")]
    [InlineData(
        @"\!\High Core R&B",
        @"\music\High Core R&B",
        "No special folder prefix falls through to \\music\\")]
    [InlineData(
        @"\!\!! -------------DOWN\-  Jammin' 100 Jingles\- 100 Countdown",
        @"\music\!! -DOWN\-  Jammin' 100 Jingles\- 100 Countdown",
        "Double-bang prefix does not match Jingle prefix, falls to \\music\\")]
    public void ParseGmlPath_KnownInputs_ReturnsExpectedPath(
        string input, string expected, string because)
    {
        var result = ParserHelpers.ParseGmlPath(input);
        Assert.Equal(expected, result);
    }

    // --- Root prefix handling ---

    [Fact]
    public void ParseGmlPath_WithoutRootPrefix_IsNotStripped()
    {
        // Input has no \!\ so the path is used as-is before folder classification
        var result = ParserHelpers.ParseGmlPath(@"\High Core R&B");
        Assert.Equal(@"\music\High Core R&B", result);
    }

    // --- DJ folder (Shows) branch ---

    [Fact]
    public void ParseGmlPath_DjPrefix_ProducesShowsPath()
    {
        var result = ParserHelpers.ParseGmlPath(@"\!\- My Show\Episode 1");
        Assert.Equal(@"\Shows\My Show\Episode 1", result);
    }

    [Fact]
    public void ParseGmlPath_DjPrefix_InnerOccurrenceOfDjPrefixIsPreserved()
    {
        // "- " appearing mid-path must NOT be replaced (the old Replace bug)
        var result = ParserHelpers.ParseGmlPath(@"\!\- ShowName\- SubFolder");
        Assert.Equal(@"\Shows\ShowName\- SubFolder", result);
    }

    // --- Jingle folder branch ---

    [Fact]
    public void ParseGmlPath_JinglePrefix_ProducesJinglesPath()
    {
        var result = ParserHelpers.ParseGmlPath(@"\!\! Station ID");
        Assert.Equal(@"\Jingles\Station ID", result);
    }

    [Fact]
    public void ParseGmlPath_JinglePrefix_InnerOccurrenceOfJinglePrefixIsPreserved()
    {
        // "! " appearing mid-path must NOT be replaced (the old Replace bug)
        var result = ParserHelpers.ParseGmlPath(@"\!\! Jingles\! NestedFolder");
        Assert.Equal(@"\Jingles\Jingles\! NestedFolder", result);
    }

    // --- Music (fallthrough) branch ---

    [Fact]
    public void ParseGmlPath_NoSpecialPrefix_ProducesMusicPath()
    {
        var result = ParserHelpers.ParseGmlPath(@"\!\SomeCategory\Track");
        Assert.Equal(@"\music\SomeCategory\Track", result);
    }

    // --- Edge cases ---

    [Fact]
    public void ParseGmlPath_EmptyAfterRootStrip_ReturnsMusicRoot()
    {
        var result = ParserHelpers.ParseGmlPath(@"\!\");
        Assert.Equal(@"\music\", result);
    }

    [Fact]
    public void ParseGmlPath_RootPrefixAppearsOnlyOnce_NotReplacedGlobally()
    {
        // \!\ mid-path should survive; only the leading one is stripped
        var result = ParserHelpers.ParseGmlPath(@"\!\Folder\!\SubFolder");
        Assert.Equal(@"\music\Folder\!\SubFolder", result);
    }
}
