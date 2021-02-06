using Simulator.Instructions;

namespace Simulator.Configuration {
    public static class CpuConfigLibrary {
        public static CpuConfigurationBuilder Cpu16Bit => Generate16Bit();
        public static CpuConfigurationBuilder Cpu32Bit => Generate32Bit();

        private static CpuConfigurationBuilder Generate32Bit() {
            var config = new CpuConfigurationBuilder()
                         .SetMemorySize(512)
                         .SetInstructionSize(32)
                         .SetOpcodeSize(8)

                         #region = registers =

                         .AddRegister("ax", 32)
                         .AddRegister("bx", 32)
                         .AddRegister("ma", 32)
                         .AddRegister("md", 32)
                         .AddRegister("ir", 32)
                         .AddRegister("pc", 32)
                         .AddRegister("halt", 1, true)

                         #endregion

                         #region = micros =

                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("ax-bx", "ax", "bx"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("bx-ax", "bx", "ax"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("pc-ma", "pc", "ma"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("md-ax", "md", "ax"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("md-bx", "md", "bx"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("md-ir", "md", "ir"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(9-16)-ax", "ir", "ax", 8, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(17-24)-ax", "ir", "ax", 16, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(25-32)-ax", "ir", "ax", 24, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(9-16)-bx", "ir", "bx", 8, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(17-24)-bx", "ir", "bx", 16, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(25-32)-bx", "ir", "bx", 24, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(9-16)-pc", "ir", "pc", 8, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(17-24)-pc", "ir", "pc", 16, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(25-32)-pc", "ir", "pc", 24, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(9-16)-ma", "ir", "ma", 8, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(17-24)-ma", "ir", "ma", 16, 24, 8))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister(
                                                  "ir(25-32)-ma", "ir", "ma", 24, 24, 8))
                         .AddMicroInstruction(MicroInstruction.MemoryRead("memr", "ma", "md", 32))
                         .AddMicroInstruction(MicroInstruction.MemoryWrite("memw", "ma", "md"))
                         .AddMicroInstruction(MicroInstruction.Set("zero-ax", "ax", 0))
                         .AddMicroInstruction(MicroInstruction.Set("zero-bx", "bx", 0))
                         .AddMicroInstruction(MicroInstruction.IoReadInt("read_i", "ax"))
                         .AddMicroInstruction(MicroInstruction.IoWriteInt("write_i", "ax"))
                         .AddMicroInstruction(MicroInstruction.RegisterCondition(
                                                  "exeq", "ax", "bx", 1, (x, y) => x == y))
                         .AddMicroInstruction(MicroInstruction.RegisterCondition(
                                                  "exne", "ax", "bx", 1, (x, y) => x != y))
                         .AddMicroInstruction(MicroInstruction.RegisterCondition(
                                                  "exgt", "ax", "bx", 1, (x, y) => x > y))
                         .AddMicroInstruction(MicroInstruction.RegisterCondition(
                                                  "exlt", "ax", "bx", 1, (x, y) => x < y))
                         .AddMicroInstruction(MicroInstruction.Add("add", "ax", "bx", "ax"))
                         .AddMicroInstruction(MicroInstruction.Subtract("sub", "ax", "bx", "ax"))
                         .AddMicroInstruction(MicroInstruction.Multiply("mul", "ax", "bx", "ax"))
                         .AddMicroInstruction(MicroInstruction.Divide("div", "ax", "bx", "ax"))
                         .AddMicroInstruction(MicroInstruction.Decode("decode", "ir"))
                         .AddMicroInstruction(MicroInstruction.Increment("pc_inc", "pc", 32))
                         .AddMicroInstruction(MicroInstruction.Set("halt", "halt", 1))

                         #endregion

                         .AddFdeCycle("pc-ma", "memr", "md-ir", "pc_inc", "decode")

                         #region = fields =

                         .AddInstructionField("ignore24", 24, CpuFieldType.Ignore)
                         .AddInstructionField("ignore16", 16, CpuFieldType.Ignore)
                         .AddInstructionField("ignore8", 8, CpuFieldType.Ignore)
                         .AddInstructionField("data24", 24, CpuFieldType.Value)
                         .AddInstructionField("data16", 16, CpuFieldType.Value)
                         .AddInstructionField("data8", 8, CpuFieldType.Value)
                         .AddInstructionField("address24", 24, CpuFieldType.Address)
                         .AddInstructionField("address16", 16, CpuFieldType.Address)
                         .AddInstructionField("address8", 8, CpuFieldType.Address)

                         #endregion

                         #region = instructions = system

                         .AddCpuInstruction("halt",
                                            CpuValue.FromInteger(0, 8),
                                            new[] {"halt"},
                                            new[] {"ignore24"})
                         .AddCpuInstruction("call",
                                            CpuValue.FromInteger(1, 8),
                                            new[] {"ir(9-16)-pc"},
                                            new[] {"address8", "ignore16"})

                         #endregion

                         #region = instructions = io

                         .AddCpuInstruction("out",
                                            CpuValue.FromInteger(2, 8),
                                            new[] {"write_i"},
                                            new[] {"ignore24"})
                         .AddCpuInstruction("in",
                                            CpuValue.FromInteger(3, 8),
                                            new[] {"read_i"},
                                            new[] {"ignore24"})
                         .AddCpuInstruction("load",
                                            CpuValue.FromInteger(4, 8),
                                            new[] {"write_i"},
                                            new[] {"ignore24"})
                         .AddCpuInstruction("save",
                                            CpuValue.FromInteger(5, 8),
                                            new[] {"read_i"},
                                            new[] {"ignore24"})

                         #endregion

                         #region = instructions = arithmetics

                         .AddCpuInstruction("add_a",
                                            CpuValue.FromInteger(6, 8),
                                            new[] {"ir(9-16)-ma", "memr", "md-bx", "add"},
                                            new[] {"address8", "ignore16"})
                         .AddCpuInstruction("sub_a",
                                            CpuValue.FromInteger(7, 8),
                                            new[] {"ir(9-16)-ma", "memr", "md-bx", "sub"},
                                            new[] {"address8", "ignore16"})
                         .AddCpuInstruction("add_v",
                                            CpuValue.FromInteger(8, 8),
                                            new[] {"ir(9-16)-bx", "add"},
                                            new[] {"data8", "ignore16"})
                         .AddCpuInstruction("sub_v",
                                            CpuValue.FromInteger(9, 8),
                                            new[] {"ir(9-16)-bx", "sub"},
                                            new[] {"data8", "ignore16"})
                         .AddCpuInstruction("mul_a",
                                            CpuValue.FromInteger(10, 8),
                                            new[] {"ir(9-16)-ma", "memr", "md-bx", "mul"},
                                            new[] {"address8", "ignore16"})
                         .AddCpuInstruction("div_a",
                                            CpuValue.FromInteger(11, 8),
                                            new[] {"ir(9-16)-ma", "memr", "md-bx", "div"},
                                            new[] {"address8", "ignore16"})
                         .AddCpuInstruction("mul_v",
                                            CpuValue.FromInteger(12, 8),
                                            new[] {"ir(9-16)-bx", "mul"},
                                            new[] {"data8", "ignore16"})
                         .AddCpuInstruction("div_v",
                                            CpuValue.FromInteger(13, 8),
                                            new[] {"ir(9-16)-bx", "div"},
                                            new[] {"data8", "ignore16"})

                         #endregion

                         #region = instructions = logic

                         .AddCpuInstruction("call_eq_a",
                                            CpuValue.FromInteger(14, 8),
                                            new[] {"ir(9-16)-ma", "memr", "md-bx", "exeq", "ir(17-24)-pc"},
                                            new[] {"address8", "address8", "ignore8"})
                         .AddCpuInstruction("call_eq_v",
                                            CpuValue.FromInteger(15, 8),
                                            new[] {"ir(9-16)-bx", "exeq", "ir(17-24)-pc"},
                                            new[] {"data8", "address8", "ignore8"})
                         .AddCpuInstruction("call_ne_a",
                                            CpuValue.FromInteger(16, 8),
                                            new[] {"ir(9-16)-ma", "memr", "md-bx", "exne", "ir(17-24)-pc"},
                                            new[] {"address8", "address8", "ignore8"})
                         .AddCpuInstruction("call_ne_v",
                                            CpuValue.FromInteger(17, 8),
                                            new[] {"ir(9-16)-bx", "exne", "ir(17-24)-pc"},
                                            new[] {"data8", "address8", "ignore8"})
                         .AddCpuInstruction("call_lt_a",
                                            CpuValue.FromInteger(18, 8),
                                            new[] {"ir(9-16)-ma", "memr", "md-bx", "exlt", "ir(17-24)-pc"},
                                            new[] {"address8", "address8", "ignore8"})
                         .AddCpuInstruction("call_lt_v",
                                            CpuValue.FromInteger(19, 8),
                                            new[] {"ir(9-16)-bx", "exlt", "ir(17-24)-pc"},
                                            new[] {"data8", "address8", "ignore8"})
                         .AddCpuInstruction("call_gt_a",
                                            CpuValue.FromInteger(20, 8),
                                            new[] {"ir(9-16)-ma", "memr", "md-bx", "exgt", "ir(17-24)-pc"},
                                            new[] {"address8", "address8", "ignore8"})
                         .AddCpuInstruction("call_gt_v",
                                            CpuValue.FromInteger(21, 8),
                                            new[] {"ir(9-16)-bx", "exgt", "ir(17-24)-pc"},
                                            new[] {"data8", "address8", "ignore8"});
                         #endregion

            return config;
        }

        private static CpuConfigurationBuilder Generate16Bit() {
            var config = new CpuConfigurationBuilder()
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
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("ax-md", "ax", "md"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("pc-ma", "pc", "ma"))
                         .AddMicroInstruction(MicroInstruction.RegisterToRegister("md-ax", "md", "ax"))
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
                         .AddMicroInstruction(MicroInstruction.IoReadChar("reads", "ax"))
                         .AddMicroInstruction(MicroInstruction.IoWriteChar("writes", "ax"))
                         .AddMicroInstruction(MicroInstruction.Set("zero-ax", "ax", 0))
                         .AddMicroInstruction(MicroInstruction.Set("zero-bx", "bx", 0))
                         .AddMicroInstruction(MicroInstruction.Set("halt", "halt", 1))
                         .AddMicroInstruction(MicroInstruction.StaticCondition("eqze", "ax", 1, i => i == 0))
                         .AddMicroInstruction(MicroInstruction.StaticCondition("neze", "ax", 1, i => i != 0))
                         .AddFdeCycle("pc-ma", "memr", "md-ir", "pc-inc", "decode")
                         .AddInstructionField("ignore8", 8, CpuFieldType.Ignore)
                         .AddInstructionField("data8", 8, CpuFieldType.Value)
                         .AddInstructionField("address8", 8, CpuFieldType.Address)
                         .AddCpuInstruction("read_int",
                                            CpuValue.FromInteger(1, 8),
                                            new[] {"read"},
                                            new[] {"ignore8"})
                         .AddCpuInstruction("write_int",
                                            CpuValue.FromInteger(2, 8),
                                            new[] {"write"},
                                            new[] {"ignore8"})
                         .AddCpuInstruction("add",
                                            CpuValue.FromInteger(3, 8),
                                            new[] {"ax-bx", "zero-ax", "ir(9-16)-ax", "add"},
                                            new[] {"data8"})
                         .AddCpuInstruction("sub",
                                            CpuValue.FromInteger(4, 8),
                                            new[] {"zero-bx", "ir(9-16)-bx", "sub"},
                                            new[] {"data8"})
                         .AddCpuInstruction("mult",
                                            CpuValue.FromInteger(5, 8),
                                            new[] {"zero-bx", "ir(9-16)-bx", "mult"},
                                            new[] {"data8"})
                         .AddCpuInstruction("div",
                                            CpuValue.FromInteger(6, 8),
                                            new[] {"zero-bx", "ir(9-16)-bx", "div"},
                                            new[] {"data8"})
                         .AddCpuInstruction("halt",
                                            CpuValue.FromInteger(7, 8),
                                            new[] {"halt"},
                                            new[] {"ignore8"})
                         .AddCpuInstruction("jump",
                                            CpuValue.FromInteger(8, 8),
                                            new[] {"ir(9-16)-pc"},
                                            new[] {"address8"})
                         .AddCpuInstruction("jeqz",
                                            CpuValue.FromInteger(9, 8),
                                            new[] {"eqze", "ir(9-16)-pc"},
                                            new[] {"address8"})
                         .AddCpuInstruction("jnez",
                                            CpuValue.FromInteger(10, 8),
                                            new[] {"neze", "ir(9-16)-pc"},
                                            new[] {"address8"})
                         .AddCpuInstruction("load",
                                            CpuValue.FromInteger(11, 8),
                                            new[] {"ir(9-16)-ma", "memr", "md-ax"},
                                            new[] {"address8"})
                         .AddCpuInstruction("save",
                                            CpuValue.FromInteger(12, 8),
                                            new[] {"ir(9-16)-ma", "ax-md", "memw"},
                                            new[] {"address8"})
                         .AddCpuInstruction("read_s",
                                            CpuValue.FromInteger(13, 8),
                                            new[] {"reads"},
                                            new[] {"ignore8"})
                         .AddCpuInstruction("write_s",
                                            CpuValue.FromInteger(14, 8),
                                            new[] {"writes"},
                                            new[] {"ignore8"});
            return config;
        }
    }
}