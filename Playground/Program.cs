using Simulator;
using Simulator.Configuration;

namespace Playground {
public static class Program {
  public static void Main(string[] args) {
    var c = CpuConfigLibrary.Cpu32Bit.Build();
    var cpu = new Cpu(c);

    const string code = @"
in

_start  call_eq_v   0       done
        sub_v       1
	    call        start

_done   out
        halt
";
    cpu.Compile(code);
    cpu.Execute();
  }
}
}