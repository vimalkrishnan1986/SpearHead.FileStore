using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpearHead.FileStore.Common.Logging
{
    public interface ILoggingService
    {
        void Log(string message);
        void Log(Exception exception);
    }
}
