namespace Simulator.Compile {
    internal class CpuToken {
        public CpuToken(string value, CpuTokenType type) {
            Value = value;
            Type = type;
        }

        internal string Value { get; }
        internal CpuTokenType Type { get; }
    }
}