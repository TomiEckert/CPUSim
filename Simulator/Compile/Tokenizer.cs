using System.Collections.Generic;
using System.Linq;

namespace Simulator.Compile {
    public static class Tokenizer {
        private static List<CpuToken> tokens;
        
        internal static IEnumerable<CpuToken> Tokenize(string code, Cpu cpu) {
            var lines = code.Split('\n').Select(x => x.Trim());

            tokens = new List<CpuToken>();

            foreach (var line in lines) {
                ProcessLine(line, cpu);
            }

            return tokens;
        }

        private static void ProcessLine(string line, Cpu cpu) {
            var parts = line.Split(' ');

            foreach (var part in parts) {
                if(part.Length < 1)
                    continue;
                
                if(CheckComment(part))
                    return;
                
                var _ = CheckLabel(part)
                        || CheckData(part)
                        || CheckInstruction(part, cpu)
                        || CheckValue(part);
            }
        }

        private static bool CheckComment(string part) {
            return Constants.CHAR_COMMENT.Contains(part);
        }

        private static bool CheckLabel(string part) {
            if (!Constants.CHAR_LABEL.Contains(part[0].ToString())) return false;
            tokens.Add(new CpuToken(part, CpuTokenType.Label));
            return true;
        }

        private static bool CheckData(string part) {
            if (!Constants.CHAR_DATA.Contains(part[0].ToString())) return false;
            tokens.Add(new CpuToken(part, CpuTokenType.Data));
            return true;
        }

        private static bool CheckInstruction(string part, Cpu cpu) {
            if (cpu.Instructions.GetInstruction(part) == null) return false;
            tokens.Add(new CpuToken(part, CpuTokenType.Instruction));
            return true;
        }

        private static bool CheckValue(string part) {
            tokens.Add(new CpuToken(part, CpuTokenType.Value));
            return true;
        }
    }
}