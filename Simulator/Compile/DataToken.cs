namespace Simulator.Compile {
    internal readonly struct DataToken {
        internal int Address { get; }
        internal int Length { get; }
        internal int Value { get; }

        public DataToken(int address, int length, int value) {
            Address = address;
            Length = length;
            Value = value;
        }
    }
}