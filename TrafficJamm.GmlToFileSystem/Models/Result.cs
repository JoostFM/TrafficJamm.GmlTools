#nullable enable

namespace TrafficJamm.GmlToFileSystem.Models;

public class Result<T> : Result
{
    public T? Data { get; set; }
}

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
