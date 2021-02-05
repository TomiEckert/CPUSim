namespace Simulator.Instructions {
    internal class ReturnBit {
        internal ReturnBitType Type { get; }
        internal int Value { get; }
        public ReturnBit(ReturnBitType type, int value) {
            Type = type;
            Value = value;
        }

        internal static ReturnBit None => new ReturnBit(ReturnBitType.None, 0);
    }

    internal enum ReturnBitType {
        None,
        OmitNextN
    }
}