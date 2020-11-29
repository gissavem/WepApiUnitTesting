using OneOf;

namespace RestAPI.Services.FileReader
{
    public abstract class ReadDirectoryResult : OneOfBase<ReadDirectoryResult.Success, ReadDirectoryResult.DirectoryNotFound>
    {
        public class Success : ReadDirectoryResult
        {
            public Success(DirectoryInfo directoryInfo)
            {
                DirectoryInfo = directoryInfo;
            }

            public DirectoryInfo DirectoryInfo { get;  }
        }

        public class DirectoryNotFound : ReadDirectoryResult
        {
        }
    }
}