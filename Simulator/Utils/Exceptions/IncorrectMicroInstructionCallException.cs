using System;

namespace Simulator.Utils.Exceptions {
[Serializable]
public class IncorrectMicroInstructionCallException : Exception {
  public IncorrectMicroInstructionCallException() {}
  public IncorrectMicroInstructionCallException(string message)
      : base(message) {}
  public IncorrectMicroInstructionCallException(string message, Exception inner)
      : base(message, inner) {}
}
}