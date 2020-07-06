using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreLogger
{
    //не уверен что так пойдет, но друго подхода не придумал. Если консоль плоха можно вообще сделать методы пустышки
    public class FakeLogger : ILogger
    {
        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Debug(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message, Exception ex)
        {
            Console.WriteLine(message, ex);
        }

        public void Error(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void Error(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Info(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void Trace(string message)
        {
            Console.WriteLine(message);
        }

        public void Trace(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }
    }
}
