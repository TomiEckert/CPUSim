using System.Collections.Generic;
using Simulator.Utils.Exceptions;

namespace Simulator.Instructions
{
    internal class MicroInstructionManager {
        internal MicroInstructionManager(IEnumerable<MicroInstruction> mi) {
            Instructions = new Dictionary<string, MicroInstruction>();
            foreach (var microInstruction in mi) {
                if (Instructions.ContainsKey(microInstruction.Name))
                    throw new MicroInstructionNameDuplicationException();
                Instructions.Add(microInstruction.Name, microInstruction);
            }
        }

        private Dictionary<string, MicroInstruction> Instructions { get; }

        internal MicroInstruction GetInstruction(string name) {
            if (Instructions.ContainsKey(name)) return Instructions[name];
            throw new IncorrectMicroInstructionCallException();
        }
    }
}