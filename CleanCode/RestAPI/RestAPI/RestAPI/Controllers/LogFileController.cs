using Microsoft.AspNetCore.Mvc;
using RestAPI.ResponseObjects;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/log-files")]
    public class LogFileController : ControllerBase
    {
        private readonly ILogReader _logReader;
        private readonly ILogFinder _logFinder;

        public LogFileController(ILogReader logReader, ILogFinder logFinder)
        {
            _logReader = logReader ?? throw new System.ArgumentNullException(nameof(logReader));
            _logFinder = logFinder ?? throw new System.ArgumentNullException(nameof(logFinder));
        }

        [HttpGet]
        public ActionResult<LogIndex> Get()
        {
            var returnIndex = new LogIndex()
            {
                LogFiles = new LogInfo[]
                {
                    new LogInfo()
                    {
                        Id = "1",
                        FileSize = 123,
                        FullName = "TheFirstLogFile"
                    },
                    new LogInfo()
                    {
                        Id = "2",
                        FileSize = 345,
                        FullName = "TheSecondLogFile"
                    }
                }
            };
            return Ok(returnIndex);
        }
        [HttpGet("{id}")]
        public ActionResult<LogFile> GetById(string id)
        {
            if (id == "1")
            {
                return Ok(new LogFile());

            }
            return NotFound();

        }
    }
}
