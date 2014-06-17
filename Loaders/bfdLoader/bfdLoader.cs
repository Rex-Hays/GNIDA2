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
        IntPtr tmp;
        [DllImport("libbfd.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr bfd_open(String filename, String target);
        [DllImport("libbfd.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern ulong bfd_entrypoint(IntPtr bfd);
        [DllImport("libbfd.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool bfd_check(String filename, String target);
        [DllImport("libbfd.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int bfd_sec_count(IntPtr bfd);
        

        public byte[] ReadBytes(long offset, int length)     
        {
            byte[] tmp = new byte[10];
            tmp[0] = 0xC3;//Костыль
            return tmp;
        }
        public SubSystem SubSystem()
        { return GNIDA.Loaders.SubSystem.WindowsConsoleUI; }
        public GNIDA.Loaders.ExecutableFlags ExecutableFlags()
        { return GNIDA.Loaders.ExecutableFlags.ExecutableFile; }
        public ulong ImageBase()
        { return 0; }
        public ulong Entrypoint()
        { 
            return bfd_entrypoint(tmp);
        }
        public List<Section> Sections()
        {
            //bfd_count_sections
  //struct bfd_section *sections;

  /* The number of sections.  */
  //unsigned int section_count;
            return new List<Section>();
        }
        public List<ExportMethod> LibraryExports()
        {
            return new List<ExportMethod>();
        }
        public List<LibraryReference> LibraryImports()
        {
            return new List<LibraryReference>();
        }
        public bool CanLoad(string FName, out string descr)
        {
            descr = "bfd Loader";
            return bfd_check(FName, "pe-i386");
        }
        public IntPtr LoadFile(string FName)
        {
            tmp = bfd_open(FName, "pe-i386");
            //bfd_map_over_sections( bfd *, &callback, void * user_data );
            Console.WriteLine(bfd_sec_count(tmp));
            return tmp;
        }
    }
}
