using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpearHead.FileStore.Common.Logging
{
    public sealed class LoggingService : ILoggingService
    {
        public void Log(string message)
        {
            Console.Write(message);
        }

        public void Log(Exception exception)
        {
            Console.Write(exception.Message);
        }
    }
}
