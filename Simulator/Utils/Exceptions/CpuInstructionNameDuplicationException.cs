using System;

namespace Simulator.Utils.Exceptions {
[Serializable]
public class CpuInstructionNameDuplicationException : Exception {
  public CpuInstructionNameDuplicationException() {}
  public CpuInstructionNameDuplicationException(string message)
      : base(message) {}
  public CpuInstructionNameDuplicationException(string message, Exception inner)
      : base(message, inner) {}
}
}
