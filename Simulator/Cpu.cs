using System;
using Simulator.Configuration;
using Simulator.Instructions;

namespace Simulator {
    public class Cpu {
        public Cpu(CpuConfiguration configuration) {
            Configuration = configuration;
            Memory = new CpuMemory(Configuration.MemorySize);
            Registers = new RegisterManager(Configuration.Registers);
            MicroInstructions = new MicroInstructionManager(Configuration.MicroInstructions);
            Instructions = new CpuInstructionManager(Configuration.CpuInstructions);
        }

        internal CpuConfiguration Configuration { get; }
        internal RegisterManager Registers { get; }
        internal CpuMemory Memory { get; }
        internal MicroInstructionManager MicroInstructions { get; }
        internal CpuInstructionManager Instructions { get; }

        public void Compile(string code) {
            Compiler.Compile(Compiler.Tokenize(code, this), this);
            if (Configuration.Debug)
                DisplayMemory();
        }

        private void DisplayMemory() {
            for (var i = 0; i < Configuration.MemorySize; i += Configuration.InstructionSize) {
                var lineNr = i / Configuration.InstructionSize + " ";
                Console.Write(lineNr.PadLeft(4, '0'));
                Console.WriteLine(Memory.GetValueAt(i, Configuration.InstructionSize).Bin);
            }

            Console.ReadLine();
            Console.Clear();
        }

        public void Execute() {
            while (true) {
                if (Registers.GetHalt().GetInt() > 0)
                    return;

                foreach (var instruction in Configuration.FdeCycle) instruction.ExecuteAction(this);
            }
        }

        internal void DisplayRegisters(string instruction) {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(Registers.GetRegisterData());
            Console.WriteLine(instruction + "            ");
            Console.ReadLine();
        }
    }
}