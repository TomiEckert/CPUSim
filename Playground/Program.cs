using Simulator;
using Simulator.Configuration;

namespace Playground {
    public static class Program {
        public static void Main(string[] args) {
            var cpu = new Cpu(CpuConfigLibrary.Cpu32Bit.Build());

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