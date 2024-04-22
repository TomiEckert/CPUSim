using System.Collections.Generic;
using System.Linq;
using Simulator.Utils.Exceptions;

namespace Simulator {
public class RegisterManager {
    private readonly Register[] _haltRegisters;
    internal RegisterManager(IEnumerable<Register> registers) {
        Registers = new Dictionary<string, Register>();

        foreach (var register in registers) {
            if (Registers.ContainsKey(register.Name))
                throw new RegisterNameDuplicationException();
            Registers.Add(register.Name, register);
        }

        _haltRegisters = Registers.Values.Where(x => x.Halt).ToArray();
    }

    private Dictionary<string, Register> Registers {
        get;
    }

    public Register this[string key] {
        get {
            if (Registers.ContainsKey(key))
                return Registers[key];
            throw new IncorrectRegisterNameException();
        }
    }

    public string GetRegisterData() {
        var texts = (from register in Registers
                     let n = register.Value.Name
                     let bin = register.Value.GetBin()
                     let i = register.Value.GetInt()
                     select n + ": " + bin + " (" + i + ")    ").ToList();

        return string.Join('\n', texts);
    }

    public Register GetHalt() {
        return _haltRegisters.FirstOrDefault(x => x.Halt);
    }
}
}