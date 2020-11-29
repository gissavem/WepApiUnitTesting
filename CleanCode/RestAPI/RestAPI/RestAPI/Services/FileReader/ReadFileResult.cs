using OneOf;

namespace RestAPI.Services.FileReader
{
    public abstract class ReadFileResult : OneOfBase<ReadFileResult.Success, ReadFileResult.FileNotFound>
    {
        public class Success : ReadFileResult
        {
            public Success(FileInfo fileInfo)
            {
                FileInfo = fileInfo;
            }
            public FileInfo FileInfo { get; }
        }
        public class FileNotFound : ReadFileResult
        {
        }
    }
}