using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerIntParser
{
    public class IntFormatException : Exception
    {
        public IntFormatException() { }
        public IntFormatException(string message) : base(message) { }
        public IntFormatException(string message, Exception inner) : base(message, inner) { }
    }
}
