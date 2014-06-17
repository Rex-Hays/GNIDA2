using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using plugins;

namespace libopcode
{

    public class libopcDasmer : IDasmer
    {
        public UInt32 disassemble(long offset, ref medi.mediana.INSTRUCTION instr, ref medi.mediana.DISASM_INOUT_PARAMS param)
        { return 0; }
    }
}
