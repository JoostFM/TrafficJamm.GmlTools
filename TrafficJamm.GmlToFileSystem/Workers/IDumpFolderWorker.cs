namespace TrafficJamm.GmlToFileSystem.Workers
{
    public interface IDumpFolderWorker
    {
        string GetLatestFileFromFolder(string folderPath);
    }
}