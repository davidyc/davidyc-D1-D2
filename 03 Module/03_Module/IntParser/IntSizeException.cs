using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerIntParser
{
    public class IntSizeException : Exception
    {
        public IntSizeException() { }
        public IntSizeException(string message) : base(message) { }
        public IntSizeException(string message, Exception inner) : base(message, inner) { }

    }
}
