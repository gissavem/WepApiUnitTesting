namespace RestAPI.Services.FileReader
{
    public class FileMetaData
    {
        
        public FileMetaData(long size, string name)
        {
            Size = size;
            Name = name;
            
        }
        public long Size { get; }
        public string Name { get; }
    }
}