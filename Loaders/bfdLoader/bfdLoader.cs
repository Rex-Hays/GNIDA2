using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNIDA.Loaders;
using plugins;
using System.Runtime.InteropServices;

namespace bfdLoader
{
    public class bfdLoader : ILoader 
    {
        [DllImport("libbfd.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern void bfd_open(String filename, String target);
        //[DllImport("libbfd.dll", CallingConvention = CallingConvention.StdCall)]
        //public static extern int fnlibbfd();
        [DllImport("libbfd.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern ulong bfd_entrypoint();
        [DllImport("libbfd.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool bfd_check(String filename, String target);
        

        public byte[] ReadBytes(long offset, int length)
        { return new byte[10]; }
        public SubSystem SubSystem()
        { return GNIDA.Loaders.SubSystem.WindowsConsoleUI; }
        public GNIDA.Loaders.ExecutableFlags ExecutableFlags()
        { return GNIDA.Loaders.ExecutableFlags.ExecutableFile; }
        public ulong ImageBase()
        { return 0; }
        public ulong Entrypoint()
        { 
            return bfd_entrypoint();
        }
        public List<Section> Sections()
        { return new List<Section>(); }
        public List<ExportMethod> LibraryExports()
        {
            return new List<ExportMethod>();
        }
        public List<LibraryReference> LibraryImports()
        {
            return new List<LibraryReference>();
        }
        public bool CanLoad(string FName)
        {
            return bfd_check(FName, null);
        }
        public void LoadFile(string FName)
        {
            bfd_open(FName, null);
        }
    }
}
