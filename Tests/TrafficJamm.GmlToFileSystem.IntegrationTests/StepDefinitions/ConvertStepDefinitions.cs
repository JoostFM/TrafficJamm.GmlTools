using System.IO.Abstractions;
using System.Text.Json;
using GmlToFileSystem.Library.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TrafficJamm.GmlToFileSystem;
using TrafficJamm.GmlToFileSystem.Models;
using TrafficJamm.GmlToFileSystem.Process;
using Xunit;

namespace TrafficJamm.GmlToFileSystem.IntegrationTests.StepDefinitions
{
    [Binding]
    public sealed class ConvertStepDefinitions
    {
        private static readonly string[] SourceFileNames =
        [
            "00008966.wav",
            "00008B61.wav",
            "00008CF7.wav",
            "00008D2D.wav",
            "00008D2E.wav",
            "00008D2F.wav",
            "00008D31.wav",
            "00008D32.wav",
            "00008D3B.wav"
        ];

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly string _workspaceDirectory = Path.Combine(Path.GetTempPath(), "TrafficJamm.IntegrationTests", Guid.NewGuid().ToString("N"));
        private readonly string _dumpDirectory;
        private readonly string _sourceDirectory;
        private readonly string _targetDirectory;
        private Result? _result;

        public ConvertStepDefinitions()
        {
            _dumpDirectory = Path.Combine(_workspaceDirectory, "dump");
            _sourceDirectory = Path.Combine(_workspaceDirectory, "source");
            _targetDirectory = Path.Combine(_workspaceDirectory, "target");
        }

        [Given("the GML dump is staged")]
        public void GivenTheGmlDumpIsStaged()
        {
            Directory.CreateDirectory(_dumpDirectory);
            Directory.CreateDirectory(_sourceDirectory);
            Directory.CreateDirectory(_targetDirectory);

            var dumpFile = Path.Combine(AppContext.BaseDirectory, "Resources", "20261109.GML.DbDump.xml");
            File.Copy(dumpFile, Path.Combine(_dumpDirectory, Path.GetFileName(dumpFile)));
        }

        [Given("the source folder contains all nine matching audio files")]
        public void GivenTheSourceFolderContainsAllMatchingAudioFiles()
        {
            foreach (var fileName in SourceFileNames)
            {
                File.WriteAllBytes(Path.Combine(_sourceDirectory, fileName), []);
            }
        }

        [Given("six tracks already exist in the target collection")]
        public void GivenSixTracksAlreadyExistInTheTargetCollection()
        {
            var trackListFile = Path.Combine(AppContext.BaseDirectory, "Resources", "fileSystemTrackList.json");
            var targetTrackListFile = Path.Combine(_targetDirectory, "fileSystemTrackList.json");
            File.Copy(trackListFile, targetTrackListFile);

            var existingTracks = JsonSerializer.Deserialize<List<TrackInfoModel>>(File.ReadAllText(targetTrackListFile), JsonOptions)
                ?? throw new InvalidOperationException("The filesystem track list fixture could not be read.");
            Assert.Equal(6, existingTracks.Count);

            foreach (var track in existingTracks)
            {
                var targetFile = Path.Combine(_targetDirectory, track.Reference);
                Directory.CreateDirectory(Path.GetDirectoryName(targetFile)!);
                byte[] previousContents = track.Metadata["GmlFilename"] == "00008CF7.wav" ? [1] : [];
                File.WriteAllBytes(targetFile, previousContents);
            }
        }

        [When("conversion is executed")]
        public async Task WhenConversionIsExecuted()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddSingleton<IFileSystem, FileSystem>();
            services.AddGmlToFileSystemServices();

            using var provider = services.BuildServiceProvider();
            var processor = provider.GetRequiredService<IGmlToFileSystemProcessor>();
            _result = await processor.ProcessAsync(
            [
                "convert",
                "--dumpfilefolder", _dumpDirectory,
                "--source", _sourceDirectory,
                "--target", _targetDirectory
            ]);
        }

        [Then("the three missing tracks and the modified track are transferred")]
        public void ThenTheThreeMissingTracksAndTheModifiedTrackAreTransferred()
        {
            Assert.NotNull(_result);
            Assert.True(_result.Success, _result.Message);

            var targetTrackListFile = Path.Combine(_targetDirectory, "fileSystemTrackList.json");
            var targetTracks = JsonSerializer.Deserialize<List<TrackInfoModel>>(File.ReadAllText(targetTrackListFile), JsonOptions)
                ?? throw new InvalidOperationException("The generated filesystem track list could not be read.");
            Assert.Equal(9, targetTracks.Count);
            Assert.Equal(SourceFileNames.Order(), targetTracks.Select(track => track.Metadata["GmlFilename"]).Order());

            foreach (var track in targetTracks)
            {
                Assert.True(File.Exists(Path.Combine(_targetDirectory, track.Reference)), $"Expected converted file for {track.Reference}.");
            }

            var modifiedTrack = Assert.Single(targetTracks, track => track.Metadata["GmlFilename"] == "00008CF7.wav");
            Assert.Empty(File.ReadAllBytes(Path.Combine(_targetDirectory, modifiedTrack.Reference)));

            var trackIndexFiles = Directory.GetFiles(_targetDirectory, "GmlTrackIndex_*.json");
            var trackIndex = Assert.Single(trackIndexFiles);
            var indexedTracks = JsonSerializer.Deserialize<List<TrackInfoModel>>(File.ReadAllText(trackIndex), JsonOptions);
            Assert.NotNull(indexedTracks);
            Assert.Equal(9, indexedTracks.Count);
        }

        [AfterScenario]
        public void CleanUp()
        {
            if (Directory.Exists(_workspaceDirectory))
            {
                Directory.Delete(_workspaceDirectory, recursive: true);
            }
        }
    }
}
