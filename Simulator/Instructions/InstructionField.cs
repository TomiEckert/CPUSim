namespace Simulator.Instructions {
    public class InstructionField {
        public InstructionField(string name, int size, CpuFieldType type) {
            Name = name;
            Size = size;
            Type = type;
            InstructionFieldManager.Instance.AddField(this);
        }

        public string Name { get; }
        public int Size { get; }
        public CpuFieldType Type { get; }
    }

    public enum CpuFieldType {
        Ignore,
        Value,
        Address
    }
}