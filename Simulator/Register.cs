namespace Simulator {
    public class Register {
        private Register(string name, int size, bool halt = false) {
            Name = name;
            Size = size;
            _value = new CpuValue("0", Size);
            Halt = halt;
        }

        public string Name { get; }
        public int Size { get; }
        public bool Halt { get; }
        private CpuValue _value;

        public string GetBin() {
            return _value.Bin;
        }

        public int GetInt() {
            return _value.Int;
        }

        public void SetBin(string binary) {
            _value.Bin = binary;
        }

        public void SetInt(int integer) {
            _value.Int = integer;
        }

        public static Register Create(string name, int blockSize) {
            return new Register(name, blockSize);
        }

        public static Register CreateHalt(string name, int blockSize) {
            return new Register(name, blockSize, true);
        }
    }
}