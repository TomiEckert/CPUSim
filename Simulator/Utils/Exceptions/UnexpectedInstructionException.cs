using System;

namespace Simulator.Utils.Exceptions
{
[Serializable]
public class UnexpectedInstructionException : Exception
{
    public UnexpectedInstructionException() { }
    public UnexpectedInstructionException(string message) : base(message) { }
    public UnexpectedInstructionException(string message, Exception inner)
        : base(message, inner) { }
}
}