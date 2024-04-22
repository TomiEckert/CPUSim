using System;
using System.Runtime.Serialization;

namespace Simulator.Configuration {
[Serializable]
public class RequiredFieldsNotAddedException : Exception {
  public RequiredFieldsNotAddedException() {}
  public RequiredFieldsNotAddedException(string message) : base(message) {}
  public RequiredFieldsNotAddedException(string message, Exception inner)
      : base(message, inner) {}
}
}