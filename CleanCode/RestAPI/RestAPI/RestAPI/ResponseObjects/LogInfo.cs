using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI.ResponseObjects
{
    public class LogInfo
    {
        public string Id { get; set; }
        public long FileSize { get; set; }
        public string FullName { get; set; }
    }
}
