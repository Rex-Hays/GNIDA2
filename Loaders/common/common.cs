using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNIDA;
using GNIDA.Loaders;

namespace plugins
{
    public interface ILoader
    {
        SubSystem SubSystem();
//        ulong SubSystem();
        GNIDA.Loaders.ExecutableFlags ExecutableFlags();
//        ulong ExecutableFlags();
        ulong ImageBase();
        ulong Entrypoint();
        List<Section> Sections();
        List<ExportMethod> LibraryExports();
        List<LibraryReference> LibraryImports();
        byte[] ReadBytes(long offset, int length);
        bool CanLoad(string FName);
        void LoadFile(string FName);
    }
    public interface IDasmer
    {
    }
}
