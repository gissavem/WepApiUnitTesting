using Microsoft.AspNetCore.Mvc;
using Moq;
using RestAPI.Controllers;
using RestAPI.ResponseObjects;
using System.Collections.Generic;
using FluentAssertions;
using RestAPI.Services.LogFinder;
using Xunit;

namespace RestAPITests
{
    public class LogFileControllerTests
    {
        [Fact]
        public void GetLogFile_ShouldReturnLogFileIndex()
        {
            //Arrange
            const string expectedValue = "fakeFileName";
            var mockLogFinder = new Mock<ILogFinder>();
            mockLogFinder
                .Setup(x => x.GetLogIndex())
                .Returns(GetTestLogIndex());
            
            var controller = new LogFileController(mockLogFinder.Object);

            //Act
            var result = controller.Get();
            
            //Assert
            //xUnit
            var actionResult = Assert.IsType<ActionResult<LogIndex>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var logIndex = Assert.IsType<LogIndex>(okResult.Value);
            Assert.Equal(expectedValue,logIndex.LogFiles[0].FullName);
            //xUnit
            
            //FluentAssertion
            result.Should().BeOfType<ActionResult<LogIndex>>();
            var fluentActionResult = result.As<ActionResult<LogIndex>>();
            
            fluentActionResult.Result.Should().BeOfType<OkObjectResult>();
            var fluentOkResult = fluentActionResult.Result.As<OkObjectResult>();

            fluentOkResult.Value.Should().BeOfType<LogIndex>();
            var fluentLogIndex = fluentOkResult.Value.As<LogIndex>();
            
            fluentLogIndex.LogFiles[0].FullName.Should().Be(expectedValue);
            //FluentAssertion
        }
        [Fact]
        public void GetLogFileById_Exists_ShouldReturnSpecifiedLogContent()
        {
            //Arrange so that there is a mocked directory which contains mocked file
            const string id = "1";
            var logFileContent = "This is the content of the log.";
            var logFile = new LogFile(logFileContent);


            var mockLogFinder = new Mock<ILogFinder>();
            mockLogFinder
                .Setup(x => x.GetLogFile(id))
                .Returns(new GetLogFileResult.Success(logFile));

            
            var controller = new LogFileController(mockLogFinder.Object);
            
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
                    new LogInfo("id",123, "fakeFileName")
                }
            };
        }
    }
}
