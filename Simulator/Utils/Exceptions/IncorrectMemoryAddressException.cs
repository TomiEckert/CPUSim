using System;

namespace Simulator.Utils.Exceptions
{
[Serializable]
public class IncorrectMemoryAddressException : Exception
{
    public IncorrectMemoryAddressException() { }
    public IncorrectMemoryAddressException(string message) : base(message) { }
    public IncorrectMemoryAddressException(string message, Exception inner)
        : base(message, inner) { }
}
}