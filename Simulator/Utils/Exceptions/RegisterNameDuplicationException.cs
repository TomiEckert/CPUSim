using System;

namespace Simulator.Utils.Exceptions
{
[Serializable]
public class RegisterNameDuplicationException : Exception
{
    public RegisterNameDuplicationException() { }
    public RegisterNameDuplicationException(string message) : base(message) { }
    public RegisterNameDuplicationException(string message, Exception inner)
        : base(message, inner) { }
}
}