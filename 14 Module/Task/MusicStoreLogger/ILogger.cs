using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLogger
{
    public interface ILogger
    {
        void Trace(string message);
        void Trace(string message, params object[] args);

        void Info(string message);
        void Info(string message, params object[] args);
        
        void Debug(string message);
        void Debug(string message, params object[] args);

        void Error(string message);
        void Error(string message, Exception ex);
        void Error(string message, params object[] args);
        void Error(Exception ex);
    }
}
