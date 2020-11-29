using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestAPI
{
    public interface ILogReader
    {
        public string GetLogContent();
    }
}
