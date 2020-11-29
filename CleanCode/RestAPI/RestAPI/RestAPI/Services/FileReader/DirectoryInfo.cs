using System.Collections.Generic;

namespace RestAPI.Services.FileReader
{
    public class DirectoryInfo
    {
        public List<FileMetaData> Files { get; set; } = new List<FileMetaData>(); 
    }
}