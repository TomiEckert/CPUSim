using System.Collections.Generic;
using System.Linq;
using Simulator.Instructions;
using Simulator.Utils;

namespace Simulator.Configuration {
    public class CpuConfigurationBuilder {
        public CpuConfigurationBuilder() {
            _registers = new List<Register>();
            _microInstructions = new List<MicroInstruction>();
            _fdeCycle = new List<MicroInstruction>();
            _instructions = new List<CpuInstruction>();
        }

        private readonly List<MicroInstruction> _fdeCycle;
        private readonly List<CpuInstruction> _instructions;
        private readonly List<MicroInstruction> _microInstructions;
        private readonly List<Register> _registers;
        private bool _debug;
        private int _instructionSize;
        private int _memorySize;
        private int _opcodeSize;

        public CpuConfigurationBuilder SetOpcodeSize(int size) {
            _opcodeSize = size;
            return this;
        }

        public CpuConfigurationBuilder SetMemorySize(int size) {
            _memorySize = size;
            return this;
        }

        public CpuConfigurationBuilder SetInstructionSize(int size) {
            _instructionSize = size;
            return this;
        }

        public CpuConfigurationBuilder EnableDebug() {
            _debug = true;
            return this;
        }

        public CpuConfigurationBuilder AddRegister(string name, int blockSize, bool halt = false) {
            _registers.Add(halt ? Register.CreateHalt(name, blockSize) : Register.Create(name, blockSize));
            return this;
        }

        public CpuConfigurationBuilder AddMicroInstruction(MicroInstruction mi) {
            _microInstructions.Add(mi);
            return this;
        }

        public CpuConfigurationBuilder AddInstructionField(string name, int size, CpuFieldType type) {
            var _ = new InstructionField(name, size, type);
            return this;
        }

        public CpuConfigurationBuilder AddCpuInstruction(string name, CpuValue opCode, string[] micros, string[] fields) {
            _instructions.Add(new CpuInstruction(name, opCode, micros, fields));
            return this;
        }

        public CpuConfigurationBuilder AddFdeCycle(params string[] microInstructions) {
            foreach (var instruction in microInstructions) {
                var mi = _microInstructions.Where(x => x.Name == instruction);
                var instructions = mi as MicroInstruction[] ?? mi.ToArray();
                if (instructions.Length != 1)
                    throw new IncorrectMicroInstructionCallException();
                _fdeCycle.Add(instructions.First());
            }

            return this;
        }

        public CpuConfiguration Build() {
            VerifyRequiredFields();
            return new CpuConfiguration(_memorySize, _opcodeSize, _instructionSize, _debug, _registers,
                                        _microInstructions,
                                        _fdeCycle, _instructions);
        }

        private void VerifyRequiredFields() {
            var verified = _instructionSize != 0;
            if (_opcodeSize == 0)
                verified = false;
            if (_memorySize == 0)
                verified = false;

            if (!verified)
                throw new CpuConfigurationExceptions.RequiredFieldsNotAddedException();
        }
    }
}