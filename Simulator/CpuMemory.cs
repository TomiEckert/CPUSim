using System.Collections.Generic;
using System.Linq;
using Simulator.Utils.Exceptions;

// ReSharper disable MemberCanBeMadeStatic.Local

namespace Simulator {
public class CpuMemory {
  public CpuMemory(int size) {
    MemorySize = size;
    Blocks = new List<CpuBinary>();
    GenerateMemory();
  }

  private List<CpuBinary> Blocks { get; }
  private int MemorySize { get; }

  public CpuValue GetValueAt(int address, int length) {
    if (address < 0 || address >= MemorySize)
      throw new IncorrectMemoryAddressException();
    return CpuValue.FromBinary(Blocks.Skip(address).Take(length));
  }

  public void SetValue(CpuValue value, int address) {
    if (address < 0 || address >= MemorySize)
      throw new IncorrectMemoryAddressException();

    for (var i = address; i < value.Size + address; i++)
      Blocks[i] =
          value.Bin[i - address] == '1' ? CpuBinary.One : CpuBinary.Zero;
  }

  private void GenerateMemory() {
    for (var i = 0; i < MemorySize; i++)
      Blocks.Add(CpuBinary.Zero);
  }
}
}