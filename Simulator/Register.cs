namespace Simulator {
    public class Register {
        private Register(string name, int size, bool halt = false) {
            Name = name;
            Size = size;
            Value = new CpuValue("0", Size);
            Halt = halt;
        }

        public string Name { get; }
        public int Size { get; }
        public bool Halt { get; }
        private CpuValue Value { get; }

        public string GetBin() {
            return Value.Bin;
        }

        public int GetInt() {
            return Value.Int;
        }

        public void SetBin(string binary) {
            Value.SetBinary(binary);
        }

        public void SetInt(int integer) {
            Value.SetInteger(integer);
        }

        public static Register Create(string name, int blockSize) {
            return new Register(name, blockSize);
        }

        public static Register CreateHalt(string name, int blockSize) {
            return new Register(name, blockSize, true);
        }
    }
}