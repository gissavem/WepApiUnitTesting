using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using RestAPI.Options;
using RestAPI.ResponseObjects;
using RestAPI.Services.FileReader;

namespace RestAPI.Services.LogFinder
{
    public class LogFinder : ILogFinder
    {
        private readonly IFileReader _fileReader;
        private readonly LogFinderOptions _logFinderOptions;
        public LogFinder(IOptions<LogFinderOptions> logFinderOptions, IFileReader fileReader)
        {
            _fileReader = fileReader;
            _logFinderOptions = logFinderOptions.Value;
        }
        
        public GetLogFileResult GetLogFile(string id)
        {
            var filePath = _logFinderOptions.FullPath + id;
            var fileContent = _fileReader.ReadFile(filePath);

            return fileContent.Match<GetLogFileResult>(
                successResult =>
                {
                    var logFile = new LogFile(successResult.FileInfo.Content);
                    return new GetLogFileResult.Success(logFile);
                },
                notFoundResult => new GetLogFileResult.LogFileNotFound());
        }
        
        public LogIndex GetLogIndex()
        {
            var basePath = _logFinderOptions.AbsoluteBaseDirectoryPath!;
            var directoryResult = _fileReader.ReadDirectory(basePath);

            return directoryResult.Match(
                successResult =>
                {
                    var logIndex = MapToLogIndex(successResult.DirectoryInfo.Files);

                    return logIndex;
                },
                notFound => new LogIndex());
        }

        private LogIndex MapToLogIndex(IEnumerable<FileMetaData> files)
        {
            var logIndex = new LogIndex();
            foreach (var file in files)
            {
                string fileId;
                try
                {
                    fileId = GetFileId(file.Name);
                }
                catch (Exception e)
                {
                    continue;
                }

                logIndex.LogFiles.Add(new LogInfo(fileId, file.Size, file.Name));
            }
            return logIndex;
        }

        private string GetFileId(string fileName)
        {
            var match = _logFinderOptions.FileIdRegex.Match(fileName);
            if (!match.Success)
            {
                throw new Exception();
            }
            var fileId = match.Groups["id"].Value;
            return fileId;
        }

    }
}