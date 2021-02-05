using System.Collections.Generic;
using System.Linq;

namespace Simulator.Instructions {
    internal class InstructionFieldManager {
        private InstructionFieldManager() {
            _fields = new List<InstructionField>();
        }

        internal static readonly InstructionFieldManager Instance = new InstructionFieldManager();

        private readonly List<InstructionField> _fields;

        internal void AddField(InstructionField field) {
            _fields.Add(field);
        }

        internal InstructionField GetField(string name) {
            return _fields.FirstOrDefault(x => x.Name == name);
        }
    }
}