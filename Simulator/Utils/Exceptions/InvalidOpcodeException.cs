using System;

namespace Simulator.Utils.Exceptions {
[Serializable]
public class InvalidOpcodeException : Exception {
  public InvalidOpcodeException() {}
  public InvalidOpcodeException(string message) : base(message) {}
  public InvalidOpcodeException(string message, Exception inner)
      : base(message, inner) {}
}
}
