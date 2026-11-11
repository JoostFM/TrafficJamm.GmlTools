using GmlToFileSystem.Library.Models;
using TrafficJamm.GmlToFileSystem.Workers;

namespace TrafficJamm.GmlToFileSystem.Tests.Workers;

public class TrackListFilterWorkerTests
{
    private readonly TrackListFilterWorker _worker = new();

    [Fact]
    public void GetItemsToUpdate_WhenFileSystemListIsEmpty_ReturnsAllGmlTracks()
    {
        var newTrack = CreateTrack("new.mp3", new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero));
        var anotherNewTrack = CreateTrack("another.mp3", new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.Zero));

        var result = _worker.GetItemsToUpdate([newTrack, anotherNewTrack], []);

        Assert.Equal([newTrack, anotherNewTrack], result);
    }

    [Fact]
    public void GetItemsToUpdate_ReturnsNewerAndMissingTracksOnly()
    {
        var olderOnGml = CreateTrack("older.mp3", new DateTimeOffset(2025, 1, 1, 0, 0, 0, TimeSpan.Zero));
        var newerOnGml = CreateTrack("newer.mp3", new DateTimeOffset(2025, 1, 3, 0, 0, 0, TimeSpan.Zero));
        var sameDate = CreateTrack("same.mp3", new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.Zero));
        var missingFromFileSystem = CreateTrack("missing.mp3", new DateTimeOffset(2025, 1, 4, 0, 0, 0, TimeSpan.Zero));
        var fileSystemTracks = new List<TrackInfoModel>
        {
            CreateTrack("older.mp3", new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.Zero)),
            CreateTrack("newer.mp3", new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.Zero)),
            CreateTrack("same.mp3", new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.Zero))
        };

        var result = _worker.GetItemsToUpdate(
            [olderOnGml, newerOnGml, sameDate, missingFromFileSystem],
            fileSystemTracks);

        Assert.Equal([newerOnGml, missingFromFileSystem], result);
    }

    private static TrackInfoModel CreateTrack(string filename, DateTimeOffset dateModified) => new()
    {
        CategoryPath = "tracks",
        Filename = filename,
        DateModified = dateModified
    };
}
