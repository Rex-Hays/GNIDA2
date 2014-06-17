using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNIDA;
using GNIDA.Loaders;
using medi;

namespace plugins
{
    public interface IGNIDA
    {
        medi.MyDictionary ToDisasmFuncList();
        medi.MyDictionary DisasmedFuncList();
        void RaiseAddFuncEvent(object sender, medi.TFunc Func);
        void RaiseVarFuncEvent(object sender, medi.TVar Var);
    }
    public interface ILoader
    {
        SubSystem SubSystem();
        GNIDA.Loaders.ExecutableFlags ExecutableFlags();
        ulong ImageBase();
        ulong Entrypoint();
        List<Section> Sections();
        List<ExportMethod> LibraryExports();
        List<LibraryReference> LibraryImports();
        byte[] ReadBytes(long offset, int length);
        bool CanLoad(string FName, out string descr);
        IntPtr LoadFile(string FName);
    }
    public interface IDasmer
   {
        UInt32 disassemble(long offset, ref medi.mediana.INSTRUCTION instr, ref medi.mediana.DISASM_INOUT_PARAMS param);
    }
}
