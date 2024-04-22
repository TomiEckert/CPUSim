using System.Collections.Generic;
using System.Linq;
using Simulator.Utils.Exceptions;

namespace Simulator.Instructions {
internal class CpuInstructionManager {
  internal CpuInstructionManager(IEnumerable<CpuInstruction> instructions) {
    Instructions = new Dictionary<CpuValue, CpuInstruction>();
    foreach (var instruction in instructions) {
      if (Instructions.ContainsKey(instruction.OpCode))
        throw new CpuInstructionNameDuplicationException();
      Instructions.Add(instruction.OpCode, instruction);
    }
  }

  private Dictionary<CpuValue, CpuInstruction> Instructions { get; }

  internal CpuInstruction GetInstruction(CpuValue opCode) {
    if (Instructions.ContainsKey(opCode))
      return Instructions[opCode];
    throw new IncorrectCpuInstructionCallException();
  }

  internal CpuInstruction GetInstruction(string name) {
    var instructions = Instructions.Values.Where(x => x.Name == name);
    return instructions.FirstOrDefault();
  }

  internal CpuValue NameToOpcode(string name) {
    var instruction = GetInstruction(name);
    if (instruction?.OpCode != null)
      return instruction.OpCode;
    throw new InvalidOpcodeException();
  }
}
}