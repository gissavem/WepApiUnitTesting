using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.ResponseObjects
{
    public class LogFile
    {
        public long FileSize { get; set; }
        public string Content { get; set; }
    }
}
