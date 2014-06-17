using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNIDA.Loaders;
using plugins;


namespace LoaderWin32
{
	public class LoaderWin32 : ILoader 
    {
        LWin32 ldr;
        public SubSystem SubSystem()
        {
            return ldr.NTHeader.OptionalHeader.SubSystem;
        }

        public GNIDA.Loaders.ExecutableFlags ExecutableFlags()
        {
            return ldr.NTHeader.FileHeader.ExecutableFlags;
        }
        public ulong ImageBase()
        {
            return ldr.NTHeader.OptionalHeader.ImageBase;
        }
        public List<ExportMethod> LibraryExports()
        {
            return ldr.LibraryExports;
        }
        public List<LibraryReference> LibraryImports()
        {
            return ldr.LibraryImports;
        }
        public byte[] ReadBytes(long offset, int length)
        {
            return ldr.Image.ReadBytes(offset, length);
        }
        public List<Section> Sections()
        {
            return ldr.NTHeader.Sections;
        }
        public ulong Entrypoint()
        {
            return ldr.NTHeader.OptionalHeader.Entrypoint.Rva;
        }
        public bool CanLoad(string FName, out string descr)
		{
            ldr = LWin32.LoadFile(FName);
            descr = "Win32 Loader";
            return (ldr.NTHeader.Signature == ImageSignature.NT);
		}
        public IntPtr LoadFile(string FName)
        {
            ldr = LWin32.LoadFile(FName);
            return IntPtr.Zero;
        }
    }
}
