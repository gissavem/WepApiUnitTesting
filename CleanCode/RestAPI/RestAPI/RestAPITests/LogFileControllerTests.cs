using Microsoft.AspNetCore.Mvc;
using Moq;
using RestAPI.Controllers;
using RestAPI.ResponseObjects;
using System.Collections.Generic;
using FluentAssertions;
using RestAPI.Services.LogFinder;
using Xunit;
using RestAPI.Services.FileReader;
using RestAPI.Options;
using Microsoft.Extensions.Options;

namespace RestAPITests
{
    public class LogFileControllerTests
    {
        [Fact]
        public void GetLogFile_ShouldReturnLogFileIndex()
        {
            //Arrange
            const string expectedName = "log_fakeFileName.json";
            const string expectedId = "fakeFileName";
            var fileInfo = new FileInfo(123, "log_fileName.json", "foobar");

            var optionsMock = new Mock<IOptions<LogFinderOptions>>();
            optionsMock.Setup(x => x.Value)
                .Returns(new LogFinderOptions());

            var fileReaderMock = new Mock<IFileReader>();
            fileReaderMock
                .Setup(x => x.ReadFile(It.IsAny<string>()))
                .Returns(new ReadFileResult.Success(fileInfo));

            var logFinderMock = new Mock<LogFinder>(optionsMock.Object, fileReaderMock.Object);
            logFinderMock
                .Setup(x => x.GetLogIndex())
                .Returns(GetTestLogIndex());
            
            var controller = new LogFileController(logFinderMock.Object);

            //Act
            var result = controller.Get();
            
            //Assert
            //xUnit
            var actionResult = Assert.IsType<ActionResult<LogIndex>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var logIndex = Assert.IsType<LogIndex>(okResult.Value);
            Assert.Equal(expectedName,logIndex.LogFiles[0].FullName);
            Assert.Equal(expectedId, logIndex.LogFiles[0].Id);
            //xUnit

            //FluentAssertion
            result.Should().BeOfType<ActionResult<LogIndex>>();
            var fluentActionResult = result.As<ActionResult<LogIndex>>();
            
            fluentActionResult.Result.Should().BeOfType<OkObjectResult>();
            var fluentOkResult = fluentActionResult.Result.As<OkObjectResult>();

            fluentOkResult.Value.Should().BeOfType<LogIndex>();
            var fluentLogIndex = fluentOkResult.Value.As<LogIndex>();
            
            fluentLogIndex.LogFiles[0].FullName.Should().Be(expectedName);
            //FluentAssertion
        }
        [Fact]
        public void GetLogFileById_Exists_ShouldReturnSpecifiedLogContent()
        {
            //Arrange so that there is a mocked directory which contains mocked file
            const string id = "fakeFileName";
            var logFileContent = "This is the content of the log.";
            var logFile = new LogFile(logFileContent);


            var logFinderMock = new Mock<ILogFinder>();
            logFinderMock
                .Setup(x => x.GetLogFile(id))
                .Returns(new GetLogFileResult.Success(logFile));
            logFinderMock
                .Setup(x => x.GetLogIndex())
                .Returns(GetTestLogIndex());

            var controller = new LogFileController(logFinderMock.Object);
            
            var actionResult = controller.GetById(id);

            actionResult.Result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = actionResult.Result.As<OkObjectResult>();
            
            okObjectResult.Value.Should().BeOfType<LogFile>();

            var responseLogFile = okObjectResult.Value.As<LogFile>();

            responseLogFile.Content.Should().Be(logFileContent);
        }
        [Fact]
        public void GetLogFileById_DoesNotExist_ShouldReturnNotFound()
        {
            const string id = "1";
            var mockLogFinder = new Mock<ILogFinder>();
            mockLogFinder
                .Setup(x => x.GetLogFile(id))
                .Returns(new GetLogFileResult.LogFileNotFound());
            
            var controller = new LogFileController(mockLogFinder.Object);
            
            var actionResult = controller.GetById(id);

            actionResult.Result.Should().BeOfType<NotFoundResult>();
        }
        private LogIndex GetTestLogIndex()
        {
            return new LogIndex()
            {
                LogFiles = new List<LogInfo>()
                {
                    new LogInfo("fakeFileName",123, "log_fakeFileName.json")
                }
            };
        }
    }
}
