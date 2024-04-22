using System;

namespace Simulator.Utils.Exceptions {
[Serializable]
public class RequiredFieldsNotAddedException : Exception {
  public RequiredFieldsNotAddedException() {}
  public RequiredFieldsNotAddedException(string message) : base(message) {}
  public RequiredFieldsNotAddedException(string message, Exception inner)
      : base(message, inner) {}
}
}
