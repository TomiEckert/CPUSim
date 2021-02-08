using System;
using System.Collections.Generic;
using System.Linq;
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
            FdeCycle = fdeCycle.ToArray();
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
        internal string CurrentInstruction { get; set; }
        internal Func<bool> WaitAction { get; set; }
        internal IEnumerable<Register> Registers { get; }
        internal IEnumerable<MicroInstruction> MicroInstructions { get; }
        internal MicroInstruction[] FdeCycle { get; }
        internal IEnumerable<CpuInstruction> CpuInstructions { get; }
    }
}