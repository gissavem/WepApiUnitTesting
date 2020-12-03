using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using RestAPI.Options;
using RestAPI.ResponseObjects;
using Xunit;
using RestAPI.Services.FileReader;
using RestAPI.Services.LogFinder;

namespace RestAPITests
{
    public class LogFinderTests
    {
        [Fact]
        public void GetLogFile_FileExists_ShouldReturnSuccess()
        {
            //Arrange
            const string logId = "123";
            var fileInfo = new FileInfo(123, "log_fileName.json", "foobar");
            
            var optionsMock = new Mock<IOptions<LogFinderOptions>>();
            optionsMock.Setup(x => x.Value)
                .Returns(new LogFinderOptions());
            
            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock
                .Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(new ReadFileResult.Success(fileInfo));
            
            var logFinder = new LogFinder(optionsMock.Object, fileReaderMock.Object);
            
            //Act
            var result = logFinder.GetLogFile(logId);

            //Assert
            result.Should().BeOfType<GetLogFileResult.Success>();
            var successResult = result.As<GetLogFileResult.Success>();
            successResult.LogFile.Content.Should().Be(fileInfo.Content);
        }
       
       
         
        [Fact]
        public void GetLogFile_FileDoesNotExist_ShouldReturnNotFound()
        {
            //Arrange
            const string id = "";
            var optionsMock = new Mock<IOptions<LogFinderOptions>>();
            optionsMock.Setup(x => x.Value)
                .Returns(new LogFinderOptions());
            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock
                .Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(new ReadFileResult.FileNotFound());
            
            var logFinder = new LogFinder(optionsMock.Object, fileReaderMock.Object);
            
            //Act
            var result = logFinder.GetLogFile(id);

            //Assert
            result.Should().BeOfType<GetLogFileResult.LogFileNotFound>();
            var notFoundResult = result.As<GetLogFileResult.LogFileNotFound>();
        }
        [Fact]
        public void GetLogFile_DirectoryDoesNotExist_ShouldReturnNotFound()
        {
            //Arrange
            const string id = "";
            var optionsMock = new Mock<IOptions<LogFinderOptions>>();
            optionsMock.Setup(x => x.Value)
                .Returns(new LogFinderOptions());
            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock
                .Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(new ReadFileResult.FileNotFound());
            
            var logFinder = new LogFinder(optionsMock.Object, fileReaderMock.Object);
            
            //Act
            var result = logFinder.GetLogFile(id);

            //Assert
            result.Should().BeOfType<GetLogFileResult.LogFileNotFound>();
            var notFoundResult = result.As<GetLogFileResult.LogFileNotFound>();
        }

        [Fact]
        public void GetLogIndex_TwoFilesExist_ShouldReturnSuccess()
        {
            //Arrange
            var directoryInfo = CreateDirectoryInfo();
            var mockOptions = new Mock<IOptions<LogFinderOptions>>();
            mockOptions
                .Setup(x => x.Value)
                .Returns(new LogFinderOptions());
            
            var fileReader = new Mock<IFileReader>();
            fileReader
                .Setup(x => x.ReadDirectory(It.IsAny<string>()))
                .Returns(new ReadDirectoryResult.Success(directoryInfo));
            
            var logFinder = new LogFinder(mockOptions.Object, fileReader.Object);
            //Act
            var result = logFinder.GetLogIndex();
            //Assert
            result.Should().BeOfType<LogIndex>();
            result.LogFiles.Should().HaveCount(2);
        }

        private DirectoryInfo CreateDirectoryInfo()
        {
            return new DirectoryInfo()
            {
                Files = new List<FileMetaData>()
                {
                    new FileMetaData(123,"log_ThisIsTheLogId.txt"),
                    new FileMetaData(356,"log_LolIdOfThisLog.json")
                }
            };
        }
    }
}