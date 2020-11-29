namespace RestAPI.Services.FileReader
{
    public interface IFileReader
    {
        ReadFileResult ReadFile(string filePath);
        ReadDirectoryResult ReadDirectory(string directoryPath);
    }
}
