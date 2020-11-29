using System;
using Microsoft.AspNetCore.Mvc;
using RestAPI.ResponseObjects;
using RestAPI.Services.LogFinder;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/log-files")]
    public class LogFileController : ControllerBase
    {
        private readonly ILogFinder _logFinder;

        public LogFileController(ILogFinder logFinder)
        {
            _logFinder = logFinder ?? throw new ArgumentNullException(nameof(logFinder));
        }

        [HttpGet]
        public ActionResult<LogIndex> Get()
        {
            var logIndex = _logFinder.GetLogIndex();
            
            return Ok(logIndex);
            
        }
        [HttpGet("{id}")]
        public ActionResult<LogFile> GetById(string id)
        {
            var logFileResult = _logFinder.GetLogFile(id);

            return logFileResult.Match<ActionResult<LogFile>>(
                successResult => Ok(successResult.LogFile), 
                notFoundResult => NotFound());
        }
    }
}
