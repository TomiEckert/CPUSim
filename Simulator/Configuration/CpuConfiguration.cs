using System.Collections.Generic;
using Simulator.Instructions;

namespace Simulator.Configuration {
    public class CpuConfiguration {
        public CpuConfiguration(int memorySize,
                                int opCodeSize,
                                int instructionSize,
                                bool debug,
                                IEnumerable<Register> registers,
                                IEnumerable<MicroInstruction> microInstructions,
                                IEnumerable<MicroInstruction> fdeCycle,
                                IEnumerable<CpuInstruction> cpuInstructions) {
            MemorySize = memorySize;
            OpCodeSize = opCodeSize;
            InstructionSize = instructionSize;
            Debug = debug;
            Registers = registers;
            MicroInstructions = microInstructions;
            FdeCycle = fdeCycle;
            CpuInstructions = cpuInstructions;
        }

        /// <summary>
        ///     Memory size in bits.
        /// </summary>
        internal int MemorySize { get; }

        /// <summary>
        ///     Opcode size in bits.
        /// </summary>
        internal int OpCodeSize { get; }

        internal bool Debug { get; }
        internal int InstructionSize { get; }
        internal IEnumerable<Register> Registers { get; }
        internal IEnumerable<MicroInstruction> MicroInstructions { get; }
        internal IEnumerable<MicroInstruction> FdeCycle { get; }
        internal IEnumerable<CpuInstruction> CpuInstructions { get; }
    }
}