using System;
using System.Runtime.Serialization;

namespace Simulator.Utils {
[Serializable]
public class IncorrectFormatException : Exception {
  public IncorrectFormatException() {}
  public IncorrectFormatException(string message) : base(message) {}
  public IncorrectFormatException(string message, Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class IncorrectValueSizeException : Exception {
  public IncorrectValueSizeException() {}
  public IncorrectValueSizeException(string message) : base(message) {}
  public IncorrectValueSizeException(string message, Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class IncorrectMemoryAddressException : Exception {
  public IncorrectMemoryAddressException() {}
  public IncorrectMemoryAddressException(string message) : base(message) {}
  public IncorrectMemoryAddressException(string message, Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class IncorrectRegisterNameException : Exception {
  public IncorrectRegisterNameException() {}
  public IncorrectRegisterNameException(string message) : base(message) {}
  public IncorrectRegisterNameException(string message, Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class RegisterNameDuplicationException : Exception {
  public RegisterNameDuplicationException() {}
  public RegisterNameDuplicationException(string message) : base(message) {}
  public RegisterNameDuplicationException(string message, Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class IncorrectMicroInstructionCallException : Exception {
  public IncorrectMicroInstructionCallException() {}
  public IncorrectMicroInstructionCallException(string message)
      : base(message) {}
  public IncorrectMicroInstructionCallException(string message, Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class IncorrectCpuInstructionCallException : Exception {
  public IncorrectCpuInstructionCallException() {}
  public IncorrectCpuInstructionCallException(string message) : base(message) {}
  public IncorrectCpuInstructionCallException(string message, Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class MicroInstructionNameDuplicationException : Exception {
  public MicroInstructionNameDuplicationException() {}
  public MicroInstructionNameDuplicationException(string message)
      : base(message) {}
  public MicroInstructionNameDuplicationException(string message,
                                                  Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class CpuInstructionNameDuplicationException : Exception {
  public CpuInstructionNameDuplicationException() {}
  public CpuInstructionNameDuplicationException(string message)
      : base(message) {}
  public CpuInstructionNameDuplicationException(string message, Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class UnexpectedInstructionException : Exception {
  public UnexpectedInstructionException() {}
  public UnexpectedInstructionException(string message) : base(message) {}
  public UnexpectedInstructionException(string message, Exception inner)
      : base(message, inner) {}
}

[Serializable]
public class InvalidOpcodeException : Exception {
  public InvalidOpcodeException() {}
  public InvalidOpcodeException(string message) : base(message) {}
  public InvalidOpcodeException(string message, Exception inner)
      : base(message, inner) {}
}
}