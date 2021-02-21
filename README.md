# CPUSIM

> Tomi Eckert

A C# library that simulates a CPU.

- [CPUSIM](#cpusim)
  - [Usage](#usage)
  - [Running code](#running-code)

## Usage

---

### Build CPU Config

Config builder usage:

```csharp
var config = new CpuConfigurationBuilder()
            .Option1(parameter)
            .Option2(parameter)
            .Build();
```

### Required options

`SetMemorySize(size)` tells the simulator the size of the memory.
> Note: low-bit CPU's can't handle large memories, as it is limited by the instruction size. For example in a 16 bit CPU the opcode is 8 bit. The `JUMP` instruction has 8 bits left for the destination address. The largest address we can fit in 8 bits is `255`.

```csharp
.SetMemorySize(128)         // 128 bit memory
```

`SetOpcodeSize(size)` tells the simulator the size of the opcode per instruction.

```csharp
.SetOpcodeSize(8)           // 8 bit opcode
```

`SetInstructionSize(size)` tells the simulator the size of the whole instruction. This is essentially the _"X bit"_ number for your CPU.

```csharp
.SetInstructionSize(16)     // 16 bit instruction set
```

---

### Setting up registers

`AddRegister(name, size)` adds a register to the CPU. Size is in bits.

```csharp
.AddRegister("ax", 16)
```

`AddRegister(name, size, halt)` adds a register to the CPU. Boolean decides if the register will be treated as a halt bit.

```csharp
.AddRegister("halt", 1, true)
```

### Adding microinstructions

`AddMicroInstruction(MicroInstruction)` adds a microinstruction to the CPU.

```csharp
.AddMicroInstruction(MicroInstruction.RegisterToRegister("ax-bx", "ax", "bx"))
```

For a list of microinstructions click [here](docs/MicroInstructions.md).

### Adding instruction fields

`AddInstructionField(name, size, type)` prepares instruction fields for the Instructions.

```csharp
.AddInstructionField("ignore8", 8, CpuFieldType.Ignore)
```

### Adding instructions

`AddCpuInstruction(name, opCode, micros, fields)` adds the instructions to the CPU. These are the instructions that will be used in the code.

```csharp
.AddCpuInstruction("ADD",
                    CpuValue.FromInteger(3, 8),
                    new[] {"ax-bx", "zero-ax", "ir(9-16)-ax", "add"},
                    new[] {"data8"})
```

## Running code

Simply construct a new Cpu with the config you've created, write code, compile and execute.

```csharp
var cpu = new Cpu(config);

const string code = @"
    READ
    ADD 1
    WRITE
    HALT
";
cpu.Compile(code);
cpu.Execute();
```
