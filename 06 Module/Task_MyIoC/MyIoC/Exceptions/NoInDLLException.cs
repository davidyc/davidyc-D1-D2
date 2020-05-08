using System;

namespace MyIoC.Exceptions
{
    public class NoInDLLException : Exception
    {
        public NoInDLLException()
        {
        }

        public NoInDLLException(string message)
            : base(message)
        {
        }

        public NoInDLLException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }

}

