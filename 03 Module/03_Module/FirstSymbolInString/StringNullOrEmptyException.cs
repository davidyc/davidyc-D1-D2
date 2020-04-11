using System;
using System.Collections.Generic;
using System.Text;

namespace FirstSymbolInString
{
    public class StringNullOrEmptyException : Exception
    {
        public StringNullOrEmptyException() { }
        public StringNullOrEmptyException(string message) : base(message) { }
        public StringNullOrEmptyException(string message, Exception inner) : base(message, inner) { }

    }
}
