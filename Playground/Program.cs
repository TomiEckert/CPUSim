using Simulator;
using Simulator.Configuration;
using Simulator.Instructions;

namespace Playground {
    public static class Program {
        public static void Main(string[] args) {
            var config = new CpuConfigurationBuilder()
                         .EnableDebug()
                         .SetMemorySize(256)
                         .SetOpcodeSize(8)
                         .SetInstructionSize(16)
                         .AddRegister("ax", 16)
                         .AddRegister("bx", 16)
                         .AddRegister("ma", 16)
                         .AddRegister("md", 16)
                         .AddRegister("pc", 16)
                         .AddRegister("ir", 16)
                         .AddRegister("halt", 1, true)
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("ax-bx", "ax", "bx"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("pc-ma", "pc", "ma"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("md-ir", "md", "ir"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("ir(9-16)-ax", "ir", "ax", 8, 8, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("ir(9-16)-bx", "ir", "bx", 8, 8, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("ir(9-16)-ma", "ir", "ma", 8, 8, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("ir(9-16)-pc", "ir", "pc", 8, 8, 8))
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
                         .AddMicroInstruction(MicroInstruction.StaticCondition("eqze", "ax", 1, i => i == 0))
                         .AddMicroInstruction(MicroInstruction.StaticCondition("neze", "ax", 1, i => i != 0))
                         .AddFdeCycle("pc-ma", "memr", "md-ir", "pc-inc", "decode")
                         .AddInstructionField("ignore8", 8, CpuFieldType.Ignore)
                         .AddInstructionField("data8", 8, CpuFieldType.Data)
                         .AddInstructionField("address8", 8, CpuFieldType.Address)
                         .AddCpuInstruction("READ",
                                            CpuValue.FromInteger(1, 8),
                                            new[] {"read"},
                                            new[] {"ignore8"})
                         .AddCpuInstruction("WRITE",
                                            CpuValue.FromInteger(2, 8),
                                            new[] {"write"},
                                            new[] {"ignore8"})
                         .AddCpuInstruction("ADD",
                                            CpuValue.FromInteger(3, 8),
                                            new[] {"ax-bx", "zero-ax", "ir(9-16)-ax", "add"},
                                            new[] {"data8"})
                         .AddCpuInstruction("SUB",
                                            CpuValue.FromInteger(4, 8),
                                            new[] {"zero-bx", "ir(9-16)-bx", "sub"},
                                            new[] {"data8"})
                         .AddCpuInstruction("MULT",
                                            CpuValue.FromInteger(5, 8),
                                            new[] {"zero-bx", "ir(9-16)-bx", "mult"},
                                            new[] {"data8"})
                         .AddCpuInstruction("DIV",
                                            CpuValue.FromInteger(6, 8),
                                            new[] {"zero-bx", "ir(9-16)-bx", "div"},
                                            new[] {"data8"})
                         .AddCpuInstruction("HALT",
                                            CpuValue.FromInteger(7, 8),
                                            new[] {"halt"},
                                            new[] {"ignore8"})
                         .AddCpuInstruction("JUMP",
                                            CpuValue.FromInteger(8, 8),
                                            new[] {"ir(9-16)-pc"},
                                            new[] {"address8"})
                         .AddCpuInstruction("JEQZ",
                                            CpuValue.FromInteger(9, 8),
                                            new[] {"eqze", "ir(9-16)-pc"},
                                            new[] {"address8"})
                         .AddCpuInstruction("JNEZ",
                                            CpuValue.FromInteger(10, 8),
                                            new[] {"neze", "ir(9-16)-pc"},
                                            new[] {"address8"})
                         .Build();

            var cpu = new Cpu(config);

            const string code = @"
READ
:Loop
JEQZ Done
WRITE
SUB 1
JUMP Loop

:Done
MULT 0
ADD 69
WRITE
HALT
";
            cpu.Compile(code);
            cpu.Execute();
        }
    }
}