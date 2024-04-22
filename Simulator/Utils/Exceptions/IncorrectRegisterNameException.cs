using System;

namespace Simulator.Utils.Exceptions
{
    [Serializable]
    public class IncorrectRegisterNameException : Exception
    {
        public IncorrectRegisterNameException() { }
        public IncorrectRegisterNameException(string message) : base(message) { }
        public IncorrectRegisterNameException(string message, Exception inner)
            : base(message, inner) { }
    }
}