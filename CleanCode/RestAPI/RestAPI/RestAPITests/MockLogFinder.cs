using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RestAPI;

namespace RestAPITests
{
    class MockLogFinder : ILogFinder
    {
        public IEnumerable<FileInfo> GetLogDirectoryFileInfo(string pathToLogDirectory)
        {
            return new DirectoryInfo(pathToLogDirectory).EnumerateFiles();   

        }
    }
}
