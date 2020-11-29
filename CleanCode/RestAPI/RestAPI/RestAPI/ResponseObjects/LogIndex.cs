using System.Collections.Generic;

namespace RestAPI.ResponseObjects
{
    public class LogIndex
    {
        public List<LogInfo> LogFiles { get; set; } = new List<LogInfo>();
    }
}
