using Simulator;
using Simulator.Configuration;
using Simulator.Instructions;

namespace Playground {
    public static class Program {
        public static void Main(string[] args) {
            var config = new CpuConfigurationBuilder()
                         .EnableDebug()
                         .SetMemorySize(128)
                         .SetOpcodeSize(8)
                         .SetInstructionSize(16)
                         .AddRegister(Register.Create("ax", 16))
                         .AddRegister(Register.Create("bx", 16))
                         .AddRegister(Register.Create("ma", 16))
                         .AddRegister(Register.Create("md", 16))
                         .AddRegister(Register.Create("pc", 16))
                         .AddRegister(Register.Create("ir", 16))
                         .AddRegister(Register.CreateHalt("halt", 1))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("ax-bx", "ax", "bx"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("pc-ma", "pc", "ma"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("md-ir", "md", "ir"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("ir(9-16)-ax", "ir", "ax", 8, 8, 8))
                         .AddMicroInstruction(MicroInstruction.MemoryRead("memr", "ma", "md", 16))
                         .AddMicroInstruction(MicroInstruction.MemoryWrite("memw", "ma", "md"))
                         .AddMicroInstruction(MicroInstruction.Increment("pc-inc", "pc", 16))
                         .AddMicroInstruction(MicroInstruction.Add("add", "ax", "bx", "ax"))
                         .AddMicroInstruction(MicroInstruction.Subtract("sub", "ax", "bx", "ax"))
                         .AddMicroInstruction(MicroInstruction.Multiply("mult", "ax", "bx", "ax"))
                         .AddMicroInstruction(MicroInstruction.Divide("div", "ax", "bx", "ax"))
                         .AddMicroInstruction(MicroInstruction.Decode("decode", "ir"))
                         .AddMicroInstruction(MicroInstruction.IoReadInt("read", "ax"))
                         .AddMicroInstruction(MicroInstruction.IoWriteInt("write", "ax"))
                         .AddMicroInstruction(MicroInstruction.Set("zero-ax", "ax", 0))
                         .AddMicroInstruction(MicroInstruction.Set("zero-bx", "bx", 0))
                         .AddMicroInstruction(MicroInstruction.Set("halt", "halt", 1))
                         .AddFdeCycle("pc-ma", "memr", "md-ir", "pc-inc", "decode")
                         .AddInstructionField(new InstructionField("ignore8", 8, CpuFieldType.Ignore))
                         .AddInstructionField(new InstructionField("data8", 8, CpuFieldType.Data))
                         .AddInstructionField(new InstructionField("address8", 8, CpuFieldType.Address))
                         .AddCpuInstruction(new CpuInstruction("READ",
                                                               CpuValue.FromInteger(1, 8),
                                                               new[] {"read"},
                                                               new[] {"ignore8"}))
                         .AddCpuInstruction(new CpuInstruction("WRITE",
                                                               CpuValue.FromInteger(2, 8),
                                                               new[] {"write"},
                                                               new[] {"ignore8"}))
                         .AddCpuInstruction(new CpuInstruction("ADD",
                                                               CpuValue.FromInteger(3, 8),
                                                               new[] {"ax-bx", "zero-ax", "ir(9-16)-ax", "add"},
                                                               new[] {"data8"}))
                         .AddCpuInstruction(new CpuInstruction("SUB",
                                                               CpuValue.FromInteger(4, 8),
                                                               new[] {"ax-bx", "zero-ax", "ir(9-16)-ax", "sub"},
                                                               new[] {"data8"}))
                         .AddCpuInstruction(new CpuInstruction("MULT",
                                                               CpuValue.FromInteger(5, 8),
                                                               new[] {"ax-bx", "zero-ax", "ir(9-16)-ax", "mult"},
                                                               new[] {"data8"}))
                         .AddCpuInstruction(new CpuInstruction("DIV",
                                                               CpuValue.FromInteger(6, 8),
                                                               new[] {"ax-bx", "zero-ax", "ir(9-16)-ax", "div"},
                                                               new[] {"data8"}))
                         .AddCpuInstruction(new CpuInstruction("HALT",
                                                               CpuValue.FromInteger(7, 8),
                                                               new[] {"halt"},
                                                               new[] {"ignore8"}))
                         .Build();

            var cpu = new Cpu(config);

            const string code = @"
READ
ADD 1
WRITE
HALT
";
            cpu.Compile(code);
            cpu.Execute();
        }
    }
}