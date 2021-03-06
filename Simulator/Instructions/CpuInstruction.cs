﻿using System.Collections.Generic;

namespace Simulator.Instructions {
    public class CpuInstruction {
        public CpuInstruction(string name, CpuValue opCode, string[] microInstructions, IReadOnlyList<string> fields) {
            MicroInstructionNames = microInstructions;
            Fields = new InstructionField[fields.Count];
            Name = name;
            OpCode = opCode;
            GetFields(fields);
        }

        public string Name { get; }
        public CpuValue OpCode { get; }
        public InstructionField[] Fields { get; }
        private string[] MicroInstructionNames { get; }

        private void GetFields(IReadOnlyList<string> fields) {
            for (var i = 0; i < fields.Count; i++) Fields[i] = InstructionFieldManager.Instance.GetField(fields[i]);
        }

        public void Execute(Cpu cpu) {
            cpu.Configuration.CurrentInstruction = Name;
            for (var i = 0; i < MicroInstructionNames.Length; i++) {
                var returnBit = cpu.MicroInstructions.GetInstruction(MicroInstructionNames[i]).ExecuteAction(cpu);
                if (returnBit.Type == ReturnBitType.OmitNextN)
                    i += returnBit.Value;
            }
        }
    }
}