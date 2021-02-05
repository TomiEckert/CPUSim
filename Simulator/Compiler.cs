using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.Instructions;
using Simulator.Utils;
// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault

namespace Simulator {
    internal static class Compiler {
        private static Stack<InstructionField> stack;
        private static Dictionary<string, int> labels;
        private static int counter;

        internal static void Compile(IEnumerable<CpuToken> tokens, Cpu cpu) {
            stack = new Stack<InstructionField>();
            labels = new Dictionary<string, int>();
            var cpuTokens = tokens.ToList();
            counter = 0;
            ProcessLabels(cpuTokens, cpu);

            foreach (var cpuToken in cpuTokens) {
                InstructionField requirement = null;

                if (stack.Any())
                    requirement = stack.Pop();

                ProcessIgnoredField(requirement);

                switch (cpuToken.Type) {
                    case CpuTokenType.Instruction:
                        ProcessInstruction(cpuToken, cpu, requirement);
                        break;
                    case CpuTokenType.Data:
                        ProcessData(cpuToken, cpu, requirement);
                        break;
                    case CpuTokenType.Label:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static void ProcessIgnoredField(InstructionField requirement) {
            if (requirement != null && requirement.Type == CpuFieldType.Ignore) counter += requirement.Size;
        }

        private static void ProcessLabels(IEnumerable<CpuToken> tokens, Cpu cpu) {
            var address = 0;
            var cpuTokens = tokens as CpuToken[] ?? tokens.ToArray();

            for (var i = 0; i < cpuTokens.Length; i++) {
                if (cpuTokens[i].Type == CpuTokenType.Instruction) {
                    var instruction = cpu.Instructions.GetInstruction(cpuTokens[i].Value);
                    address += cpu.Configuration.InstructionSize;
                    i += instruction.Fields.Count(x => x.Type != CpuFieldType.Ignore);
                }

                if (cpuTokens[i].Type == CpuTokenType.Label) labels.Add(cpuTokens[i].Value.ReplaceAll(Constants.CHAR_LABEL, ""), address);
            }
        }

        private static void ProcessData(CpuToken token, Cpu cpu, InstructionField requirement) {
            if(requirement == null)
                return;
            switch (requirement.Type) {
                case CpuFieldType.Ignore:
                    throw new UnexpectedInstructionException();
                case CpuFieldType.Data:
                    cpu.Memory.SetValue(token.Value.ToCpuValueFromInt(requirement.Size), counter);
                    counter += requirement.Size;
                    break;
                case CpuFieldType.Address when !labels.ContainsKey(token.Value):
                    throw new UnexpectedInstructionException();
                case CpuFieldType.Address:
                    cpu.Memory.SetValue(labels[token.Value].ToCpuValue(requirement.Size), counter);
                    counter += requirement.Size;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static bool IsInvalidInstruction(InstructionField requirement) {
            return requirement != null && requirement.Type != CpuFieldType.Ignore;
        }

        private static void ProcessInstruction(CpuToken token, Cpu cpu, InstructionField requirement) {
            if (IsInvalidInstruction(requirement))
                throw new UnexpectedInstructionException();

            var op = cpu.Instructions.NameToOpcode(token.Value);
            cpu.Memory.SetValue(op, counter);
            stack.PushRange(cpu.Instructions.GetInstruction(token.Value).Fields);
            counter += cpu.Configuration.OpCodeSize;
        }

        internal static IEnumerable<CpuToken> Tokenize(string code, Cpu cpu) {
            var lines = code.Split('\n').Select(x => x.Trim());

            var tokens = new List<CpuToken>();

            foreach (var line in lines) {
                var parts = line.Split(' ');
                tokens.AddRange(parts.TakeWhile(part => !Constants.CHAR_COMMENT.Contains(part) && part.Length > 0)
                                     .Select(part => {
                                         if (cpu.Instructions.GetInstruction(part) != null)
                                             return new CpuToken(part, CpuTokenType.Instruction);
                                         return Constants.CHAR_LABEL.Contains(part[0].ToString())
                                             ? new CpuToken(part, CpuTokenType.Label)
                                             : new CpuToken(part, CpuTokenType.Data);
                                     }));
            }

            return tokens;
        }
    }

    internal class CpuToken {
        public CpuToken(string value, CpuTokenType type) {
            Value = value;
            Type = type;
        }

        internal string Value { get; }
        internal CpuTokenType Type { get; }
    }

    internal enum CpuTokenType {
        // ReSharper disable once UnusedMember.Global
        None,
        Instruction,
        Data,
        Label
    }
}
