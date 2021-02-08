using System;
using System.Collections.Generic;
using System.Linq;
using Simulator.Instructions;
using Simulator.Utils;

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault

namespace Simulator.Compile {
    internal static class Compiler {
        private static Stack<InstructionField> stack;
        private static Dictionary<string, int> labels;
        private static List<DataToken> data;
        private static int counter;
        private static int index;
        private static InstructionField currentField;

        internal static void Compile(string code, Cpu cpu) {
            
            var tokens = Tokenizer.Tokenize(code, cpu).ToList();
            
            data = DataFinder.FindData(tokens, cpu);
            labels = Labeler.Label(tokens, data, cpu);

            WriteToMemory(tokens, cpu);
        }

        // ReSharper disable once CognitiveComplexity
        private static void WriteToMemory(IReadOnlyList<CpuToken> tokens, Cpu cpu) {
            stack = new Stack<InstructionField>();
            counter = 0;

            for (index = 0; index < tokens.Count; index++) {
                
                var cpuToken = tokens[index];
                currentField = null;

                if (stack.Any())
                    currentField = stack.Pop();

                switch (cpuToken.Type) {
                    case CpuTokenType.Data:
                        ProcessData(cpu, data);
                        break;
                    case CpuTokenType.Instruction:
                        ProcessInstruction(cpuToken, cpu, currentField);
                        break;
                    case CpuTokenType.Value:
                        ProcessValue(cpuToken, cpu, currentField);
                        break;
                    case CpuTokenType.Label:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        // ReSharper disable once ParameterTypeCanBeEnumerable.Local
        private static void ProcessData(Cpu cpu, List<DataToken> dataTokens) {
            var dataToken = dataTokens.FirstOrDefault(x => x.Address == counter);
            cpu.Memory.SetValue(dataToken.Value.ToCpuValue(dataToken.Length), dataToken.Address);
            counter += dataToken.Length;
            index += 3;
        }
        
        private static void ProcessValue(CpuToken token, Cpu cpu, InstructionField requirement) {
            if(requirement == null)
                return;
            switch (requirement.Type) {
                case CpuFieldType.Ignore:
                    throw new UnexpectedInstructionException();
                case CpuFieldType.Value:
                    cpu.Memory.SetValue(token.Value.ToCpuValueFromInt(requirement.Size), counter);
                    counter += requirement.Size;
                    ProcessIgnoredFields();
                    break;
                case CpuFieldType.Address:
                    ProcessAddress(cpu, token, requirement);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ProcessAddress(Cpu cpu, CpuToken token, InstructionField requirement) {
            cpu.Memory.SetValue(
                int.TryParse(token.Value, out var number)
                    ? number.ToCpuValue(requirement.Size)
                    : labels[token.Value].ToCpuValue(requirement.Size), counter);
            counter += requirement.Size;
            ProcessIgnoredFields();
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
            ProcessIgnoredFields();
            counter += cpu.Configuration.OpCodeSize;
        }

        private static void ProcessIgnoredFields() {
            while (stack.Count > 0 && stack.Peek().Type == CpuFieldType.Ignore) {
                counter += stack.Pop().Size;
            }
        }
    }
}
