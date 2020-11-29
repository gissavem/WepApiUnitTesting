using RestAPI.ResponseObjects;

namespace RestAPI.Services.LogFinder
{
    public interface ILogFinder
    {
        public LogIndex GetLogIndex();
        public GetLogFileResult GetLogFile(string id);
    }
}
