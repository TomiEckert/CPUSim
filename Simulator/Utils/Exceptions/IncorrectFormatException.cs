using System;

namespace Simulator.Utils.Exceptions {
[Serializable]
public class IncorrectFormatException : Exception {
  public IncorrectFormatException() {}
  public IncorrectFormatException(string message) : base(message) {}
  public IncorrectFormatException(string message, Exception inner)
      : base(message, inner) {}
}
}
