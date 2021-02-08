using System.Collections.Generic;
using System.Linq;
using Simulator.Instructions;

namespace Simulator.Compile {
    public static class DataFinder {
        private static List<DataToken> data;
        private static int index;
        private static int address;
        
        internal static List<DataToken> FindData(IEnumerable<CpuToken> tokens, Cpu cpu) {
            data = new List<DataToken>();
            address = 0;
            var cpuTokens = tokens as CpuToken[] ?? tokens.ToArray();

            for (index = 0; index < cpuTokens.Length; index++) {
                if (cpuTokens[index].Type == CpuTokenType.Instruction) SkipInstructions(cpu, cpuTokens);
                if (cpuTokens[index].Type == CpuTokenType.Data) ProcessData(cpuTokens);
            }

            return data;
        }

        private static void ProcessData(IReadOnlyList<CpuToken> cpuTokens) {
            if (cpuTokens[index].Type != CpuTokenType.Data) return;
            var length = int.Parse(cpuTokens[index + 1].Value);
            var value = int.Parse(cpuTokens[index + 2].Value);
            data.Add(new DataToken(address, length, value));
            address += length;
            index += 3;
        }

        private static void SkipInstructions(Cpu cpu, IReadOnlyList<CpuToken> cpuTokens) {
            var instruction = cpu.Instructions.GetInstruction(cpuTokens[index].Value);
            address += cpu.Configuration.InstructionSize;
            index += instruction.Fields.Count(x => x.Type != CpuFieldType.Ignore);
        }
    }
}