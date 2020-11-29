namespace RestAPI.ResponseObjects
{
    public class LogInfo
    {
        public LogInfo(string id, long fileSize, string fullName)
        {
            Id = id;
            FileSize = fileSize;
            FullName = fullName;
        }
        public string Id { get;  }
        public long FileSize { get;  }
        public string FullName { get;  }
    }
}
