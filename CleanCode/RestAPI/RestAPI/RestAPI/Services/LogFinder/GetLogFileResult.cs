using OneOf;
using RestAPI.ResponseObjects;

namespace RestAPI.Services.LogFinder
{
    public abstract class GetLogFileResult : OneOfBase<GetLogFileResult.Success, GetLogFileResult.LogFileNotFound>
    {
        public class Success : GetLogFileResult
        {
            public Success(LogFile logFile)
            {
                LogFile = logFile;
            }
            public LogFile LogFile { get; }
        }
        public class LogFileNotFound : GetLogFileResult
        {
            
        }
    }
}