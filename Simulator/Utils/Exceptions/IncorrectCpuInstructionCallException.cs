using System;

namespace Simulator.Utils.Exceptions {
[Serializable]
public class IncorrectCpuInstructionCallException : Exception {
  public IncorrectCpuInstructionCallException() {}
  public IncorrectCpuInstructionCallException(string message) : base(message) {}
  public IncorrectCpuInstructionCallException(string message, Exception inner)
      : base(message, inner) {}
}
}
