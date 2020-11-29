using System;

namespace RestAPI.ResponseObjects
{
    public class LogFile
    {
        public LogFile(string content)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public string Content { get; }
        
    }
}
