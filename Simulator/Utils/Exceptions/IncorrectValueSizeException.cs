using System;

namespace Simulator.Utils.Exceptions
{
    [Serializable]
    public class IncorrectValueSizeException : Exception
    {
        public IncorrectValueSizeException() { }
        public IncorrectValueSizeException(string message) : base(message) { }
        public IncorrectValueSizeException(string message, Exception inner)
            : base(message, inner) { }
    }
}