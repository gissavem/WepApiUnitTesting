using System;
using System.IO;

namespace RestAPI.Services.FileReader
{
    public class FileReader : IFileReader
    {
        public ReadFileResult ReadFile(string filePath)
        {
            FileStream fileStream;
            try
            {
                fileStream = File.Open(filePath, FileMode.Open);
            }
            catch (FileNotFoundException)
            {
                return new ReadFileResult.FileNotFound();
            }
            catch (DirectoryNotFoundException)
            {
                return new ReadFileResult.FileNotFound();
            }
            var fileSize = fileStream.Length;
            var fileName = fileStream.Name;
            string fileContent;
            using StreamReader streamReader = new StreamReader(fileStream);
            {
                fileContent = streamReader.ReadToEnd();
            }
            var fileInfo = new FileInfo(fileSize,fileName,fileContent);
            return new ReadFileResult.Success(fileInfo);
        }

        public ReadDirectoryResult ReadDirectory(string directoryPath)
        {
            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(directoryPath);
            if (!directoryInfo.Exists)
            {
                return new ReadDirectoryResult.DirectoryNotFound();
            }

            var filesInDirectory = directoryInfo.EnumerateFiles();
            DirectoryInfo mappedDirectoryInfo = new DirectoryInfo();

            foreach (var fileInfo in filesInDirectory)
            {
                mappedDirectoryInfo.Files.Add(new FileMetaData(fileInfo.Length,fileInfo.Name));
            }
            return new ReadDirectoryResult.Success(mappedDirectoryInfo);
        }
    }
}