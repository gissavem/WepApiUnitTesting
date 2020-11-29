using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI
{
    public interface ILogFinder
    {
        public IEnumerable<FileInfo> GetLogDirectoryFileInfo(string pathToLogDirectory);
        
        
    }
}
