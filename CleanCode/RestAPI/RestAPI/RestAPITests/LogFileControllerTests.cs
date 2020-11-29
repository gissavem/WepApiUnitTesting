using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestAPI;
using RestAPI.Controllers;
using RestAPI.ResponseObjects;
using System.Collections.Generic;
using System.IO;

namespace RestAPITests
{
    [TestClass]
    public class LogFileControllerTests
    {
        [TestMethod]
        public void GetLogFile_ShouldReturn_LogFileIndex()
        {
            //Arrange
            var expected = new LogIndex();
            var mockLogFinder = new Mock<ILogFinder>();

            var controller = new LogFileController(new MockLogReader(), new MockLogFinder());

            var response = controller.Get();
            var result = response.Result as OkObjectResult;
            var value = result.Value;
            Assert.IsInstanceOfType(response, typeof(ActionResult<LogIndex>));
            //Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetLogFileById_ShouldReturn_SpecifiedLogContent()
        {
            //Arrange so that there is a mocked directory which contains mocked file
            var id = "1";
            var mockLogFinder = new Mock<ILogFinder>();

            mockLogFinder
                .Setup(x => x.GetLogDirectoryFileInfo("fakePath"))
                .Returns(new List<FileInfo>() { new FileInfo(null) });

            var logContent = "This is logdagta";
            
            var controller = new LogFileController(null, null);

            var result = controller.GetById(id);
        }
    }
}
