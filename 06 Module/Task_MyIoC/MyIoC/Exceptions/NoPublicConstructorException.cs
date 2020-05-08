using System;

namespace MyIoC.Exceptions
{
    public class NoPublicConstructorException : Exception
    {
        public NoPublicConstructorException()
        {
        }

        public NoPublicConstructorException(string message)
            : base(message)
        {
        }

        public NoPublicConstructorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
