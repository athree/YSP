using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SysLog
{
    public interface ILog : IDisposable
    {
        void Write(string str);
    }
}
