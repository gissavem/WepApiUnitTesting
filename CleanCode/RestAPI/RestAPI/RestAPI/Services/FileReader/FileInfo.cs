namespace RestAPI.Services.FileReader
{
    public class FileInfo
    {
        public FileInfo(long size, string name, string content)
        {
            Size = size;
            Name = name;
            Content = content;
        }
        /// <summary>
        /// Size in bytes
        /// </summary>
        public long Size { get; }
        public string Name { get; }
        public string Content { get;  }

    }
}