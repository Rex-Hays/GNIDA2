using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNIDA.Loaders;

namespace ldrs
{
    public interface ILoader
    {
        SubSystem SubSystem();
        GNIDA.Loaders.ExecutableFlags ExecutableFlags();
        ulong ImageBase();
        uint Entrypoint();
        List<Section> Sections();
        List<ExportMethod> LibraryExports();
        List<LibraryReference> LibraryImports();
        byte[] ReadBytes(long offset, int length);
        bool CanLoad(string FName);
        void LoadFile(string FName);
    }
}
