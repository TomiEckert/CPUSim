using System;

namespace Simulator.Utils.Exceptions
{
    [Serializable]
    public class MicroInstructionNameDuplicationException : Exception
    {
        public MicroInstructionNameDuplicationException() { }
        public MicroInstructionNameDuplicationException(string message)
            : base(message) { }
        public MicroInstructionNameDuplicationException(string message,
                                                        Exception inner)
            : base(message, inner) { }
    }
}