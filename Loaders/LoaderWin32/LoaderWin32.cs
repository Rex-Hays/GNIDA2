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
        public string FName { get; set; }
        LWin32 ldr;
        public ulong SubSystem()
        {
            return (ulong)ldr.NTHeader.OptionalHeader.SubSystem;
        }

        public ulong ExecutableFlags()
        {
            return (ulong)ldr.NTHeader.FileHeader.ExecutableFlags;
        }
        public ulong ImageBase()
        {
            return ldr.NTHeader.OptionalHeader.ImageBase;
        }
        public static ExportMethod1 Le2Le(ExportMethod pf)
        {
            return new ExportMethod1(pf.RVA, pf.Ordinal, pf.Name);
        }
        public List<ExportMethod1> LibraryExports()
        {
            return ldr.LibraryExports.ConvertAll(
                new Converter<ExportMethod, ExportMethod1>(Le2Le));
        }


        public static LibraryReference1 L2L(LibraryReference lf)
        {
            LibraryReference1 tmp = new LibraryReference1(lf.LibraryName);
            tmp.ImportMethods = new ImportMethods1[lf.ImportMethods.Count()];
            for (int i = 0; i < lf.ImportMethods.Count(); i++)
            {
                tmp.ImportMethods[i] =
                    new ImportMethods1(lf.ImportMethods[i].RVA, lf.ImportMethods[i].Ordinal, lf.ImportMethods[i].Name);
            }
            return tmp;
        }
        public List<LibraryReference1> LibraryImports()
        {
            return ldr.LibraryImports.ConvertAll(
                new Converter<LibraryReference, LibraryReference1>(L2L));
            //return (List<GNIDA.Loaders.LibraryReference>)asmbly.LibraryImports;
        }
        public byte[] ReadBytes(ulong offset, int length)
        {
            return ldr.Image.ReadBytes((long)offset, length);
        }
        public static Section1 s2s(Section sc)
        {
            return new Section1(sc.RVA, sc.VirtualSize, sc.Name, sc.RawOffset, sc.RawSize);
        }
        public List<Section1> Sections()
        {
            return ldr.NTHeader.Sections.ConvertAll(
                new Converter<Section, Section1>(s2s));
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
            this.FName = FName;
            ldr = LWin32.LoadFile(FName);
            return IntPtr.Zero;
        }
    }
}
