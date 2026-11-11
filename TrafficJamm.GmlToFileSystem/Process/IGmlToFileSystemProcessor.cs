using TrafficJamm.GmlToFileSystem.Models;

namespace TrafficJamm.GmlToFileSystem.Process;

public interface IGmlToFileSystemProcessor
{
    Task<Result> ProcessAsync(string[] args);
}