using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using plugins;

namespace DasmerTUP
{
    public class Instr : IInstruction
    {
        public override string ToString(MyDictionary ProcList, VarDictionary VarDict, Dictionary<ulong, TFunc> NewSubs)
        {
            return "TO-DO";
        }
    }
    public class DasmerTUP : IDasmer
    {
        public static ILoader assembly;
        public void Init(ILoader _assembly)
        {
            assembly = _assembly;
        }
        public UInt32 disassemble(ulong offset, out IInstruction instr, ref DISASM_INOUT_PARAMS param)
        {
            instr = new Instr();
            return 0;
        }
    }
}
