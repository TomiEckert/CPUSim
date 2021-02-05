using System;
using Simulator.Utils;
// ReSharper disable UnusedMember.Global

namespace Simulator.Instructions {
    public class MicroInstruction {
        private MicroInstruction(string name, Action<Cpu> executeAction) {
            Action = cpu => {
                executeAction(cpu);
                return new ReturnBit(ReturnBitType.None, 0);
            };
            Name = name;
        }
        private MicroInstruction(string name, Func<Cpu, ReturnBit> executeAction) {
            Action = executeAction;
            Name = name;
        }

        public string Name { get; }
        private Func<Cpu, ReturnBit> Action { get; }

        internal ReturnBit ExecuteAction(Cpu cpu) {
            if (cpu.Configuration.Debug)
                cpu.DisplayRegisters(Name);
            return Action(cpu);
        }

        #region === Register To Register ===
        public static MicroInstruction RegisterToRegister(string name, string from, string to) {
            void Action(Cpu cpu) {
                cpu.Registers[to].SetBin(cpu.Registers[from].GetBin());
            }

            return new MicroInstruction(name, Action);
        }

        public static MicroInstruction RegisterToRegister(string name, string from, string to, int fromStart,
                                                          int toStart, int length) {
            void Action(Cpu cpu) {
                var r1 = cpu.Registers[from];
                var r2 = cpu.Registers[to];

                var v1 = r1.GetBin().Substring(fromStart, length);
                var v2 = r2.GetBin().Substring(0, toStart);

                var val = v2 + v1;

                cpu.Registers[to].SetBin(val);
            }

            return new MicroInstruction(name, Action);
        }
        #endregion

        #region === Memory Access ===
        public static MicroInstruction MemoryRead(string name, string addressRegister,
                                                  string dataRegister, int length) {
            void Action(Cpu cpu) {
                cpu.Registers[dataRegister].SetBin(
                    cpu.Memory.GetValueAt(cpu.Registers[addressRegister].GetInt(), length).Bin);
            }

            return new MicroInstruction(name, Action);
        }
        public static MicroInstruction MemoryWrite(string name, string addressRegister, string dataRegister) {
            void Action(Cpu cpu) {
                cpu.Memory.SetValue(
                    new CpuValue(
                        cpu.Registers[dataRegister].GetBin(), cpu.Registers[dataRegister].Size),
                    cpu.Registers[addressRegister].GetInt());
            }

            return new MicroInstruction(name, Action);
        }
        #endregion

        #region === IO Access ===
        public static MicroInstruction IoReadInt(string name, string register) {
            void Action(Cpu cpu) {
                cpu.Registers[register].SetInt(int.Parse(Console.ReadLine() ?? "0"));
            }

            return new MicroInstruction(name, Action);
        }
        public static MicroInstruction IoWriteInt(string name, string register) {
            void Action(Cpu cpu) {
                Console.WriteLine(cpu.Registers[register].GetInt());
            }

            return new MicroInstruction(name, Action);
        }
        #endregion

        #region === Set ===
        public static MicroInstruction Set(string name, string register, int number) {
            void Action(Cpu cpu) {
                cpu.Registers[register].SetInt(number);
            }

            return new MicroInstruction(name, Action);
        }
        #endregion
        
        #region === Decode ===
        public static MicroInstruction Decode(string name, string register) {
            void Action(Cpu cpu) {
                var bin = cpu.Registers[register].GetBin();
                var opcode = bin.Substring(0, cpu.Configuration.OpCodeSize);
                var value = opcode.ToCpuValueFromBin(cpu.Configuration.OpCodeSize);
                cpu.Instructions.GetInstruction(value).Execute(cpu);
            }

            return new MicroInstruction(name, Action);
        }
        #endregion

        #region === Increment ===
        public static MicroInstruction Increment(string name, string register, int number) {
            void Action(Cpu cpu) {
                var oldValue = cpu.Registers[register].GetInt();
                var newValue = oldValue + number;
                cpu.Registers[register].SetInt(newValue);
            }

            return new MicroInstruction(name, Action);
        }
        public static MicroInstruction Decrement(string name, string register, int number) {
            void Action(Cpu cpu) {
                cpu.Registers[register].SetInt(cpu.Registers[register].GetInt() - number);
            }

            return new MicroInstruction(name, Action);
        }
        #endregion
        
        #region === Arithmetics ===
        public static MicroInstruction Add(string name, string source1, string source2, string destination) {
            void Action(Cpu cpu) {
                cpu.Registers[destination]
                   .SetInt(cpu.Registers[source1].GetInt() + cpu.Registers[source2].GetInt());
            }

            return new MicroInstruction(name, Action);
        }
        public static MicroInstruction Subtract(string name, string source1, string source2, string destination) {
            void Action(Cpu cpu) {
                cpu.Registers[destination]
                   .SetInt(cpu.Registers[source1].GetInt() - cpu.Registers[source2].GetInt());
            }

            return new MicroInstruction(name, Action);
        }
        public static MicroInstruction Multiply(string name, string source1, string source2, string destination) {
            void Action(Cpu cpu) {
                cpu.Registers[destination]
                   .SetInt(cpu.Registers[source1].GetInt() * cpu.Registers[source2].GetInt());
            }

            return new MicroInstruction(name, Action);
        }
        public static MicroInstruction Divide(string name, string source1, string source2, string destination) {
            void Action(Cpu cpu) {
                cpu.Registers[destination]
                   .SetInt(cpu.Registers[source1].GetInt() / cpu.Registers[source2].GetInt());
            }

            return new MicroInstruction(name, Action);
        }
        #endregion

        #region === Logic ===
        public static MicroInstruction StaticCondition(string name, string register, int number, Func<int, bool> func) {
            ReturnBit Action(Cpu cpu) {
                return func(cpu.Registers[register].GetInt()) ? new ReturnBit(ReturnBitType.OmitNextN, number) : ReturnBit.None;
            }

            return new MicroInstruction(name, Action);
        }
        public static MicroInstruction RegisterCondition(string name, string register1, string register2, int number,
                                                         Func<int, int, bool> func) {
            ReturnBit Action(Cpu cpu) {
                var n1 = cpu.Registers[register1].GetInt();
                var n2 = cpu.Registers[register2].GetInt();
                return func(n1, n2) ? new ReturnBit(ReturnBitType.OmitNextN, number) : ReturnBit.None;
            }
            return new MicroInstruction(name, Action);
        }
        #endregion
    }
}