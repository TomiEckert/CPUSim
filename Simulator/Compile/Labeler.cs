using System.Collections.Generic;
using System.Linq;
using Simulator.Instructions;
using Simulator.Utils;

namespace Simulator.Compile {
    public class Labeler {
        private static Dictionary<string, int> labels;
        private static int index;
        private static int address;

        internal static Dictionary<string, int> Label(IEnumerable<CpuToken> tokens, IEnumerable<DataToken> data, Cpu cpu) {
            labels = new Dictionary<string, int>();
            address = 0;
            var cpuTokens = tokens as CpuToken[] ?? tokens.ToArray();
            var dataArray = data as DataToken[] ?? data.ToArray();

            for (index = 0; index < cpuTokens.Length; index++) {
                var _ = ProcessInstructions(cpu, cpuTokens)
                        || ProcessData(cpuTokens, dataArray)
                        || ProcessLabel(cpuTokens);
            }

            return labels;
        }

        private static bool ProcessLabel(IReadOnlyList<CpuToken> cpuTokens) {
            if (cpuTokens[index].Type != CpuTokenType.Label) return false;
            labels.Add(cpuTokens[index].Value.ReplaceAll(Constants.CHAR_LABEL, ""), address);
            return true;
        }
        private static bool ProcessData(CpuToken[] cpuTokens, IEnumerable<DataToken> data) {
            if (cpuTokens[index].Type != CpuTokenType.Data) return false;
            var length = data.Where(x => x.Address == address).Select(x => x.Length).FirstOrDefault();
            index += 3;
            address += length;
            return true;
        }
        private static bool ProcessInstructions(Cpu cpu, IReadOnlyList<CpuToken> cpuTokens) {
            if (cpuTokens[index].Type != CpuTokenType.Instruction) return false;
            var instruction = cpu.Instructions.GetInstruction(cpuTokens[index].Value);
            address += cpu.Configuration.InstructionSize;
            index += instruction.Fields.Count(x => x.Type != CpuFieldType.Ignore);
            return true;
        }
    }
}