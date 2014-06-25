using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using plugins;
using TUP;
using TUP.AsmResolver;
using GNIDA;

namespace TUPLoader
{
    public class TUPLoader : ILoader
    {
        public string FName { get; set; }
        Win32Assembly asmbly;
        public static Section1 s2s(Section sc)
        {
            return new Section1(sc.RVA, sc.VirtualSize, sc.Name, sc.RawOffset,sc.RawSize);
        }
        public List<Section1> Sections()
        {
            return asmbly.NTHeader.Sections.ConvertAll(
                new Converter<Section,Section1>(s2s));
        }

        public static ExportMethod1 Le2Le(ExportMethod pf)
        {
            return new ExportMethod1(pf.RVA, pf.Ordinal, pf.Name);
        }
        public List<ExportMethod1> LibraryExports()
        {
            return asmbly.LibraryExports.ConvertAll(
                new Converter<ExportMethod, ExportMethod1>(Le2Le));
        }
        public static LibraryReference1 L2L(LibraryReference lf)
        {
            LibraryReference1 tmp = new LibraryReference1(lf.LibraryName);
            for (int i = 0; i < lf.ImportMethods.Count(); i++)
            {
                tmp.ImportMethods[i] = 
                    new ImportMethods1(lf.ImportMethods[i].RVA, lf.ImportMethods[i].Ordinal, lf.ImportMethods[i].Name);
            }
            return tmp;
        }
        public List<LibraryReference1> LibraryImports()
        {
            return asmbly.LibraryImports.ConvertAll(
                new Converter<LibraryReference, LibraryReference1>(L2L));
            //return (List<GNIDA.Loaders.LibraryReference>)asmbly.LibraryImports;
        }
        public byte[] ReadBytes(ulong offset, int length)
        {
            return asmbly.Image.ReadBytes((int)offset, length);
        }
        public ulong ExecutableFlags()
        {
            return (ulong)asmbly.NTHeader.FileHeader.ExecutableFlags;
        }
        public ulong ImageBase()
        {
            return asmbly.NTHeader.OptionalHeader.ImageBase;
        }
        public ulong Entrypoint()
        {
            return asmbly.NTHeader.OptionalHeader.Entrypoint.Rva;
        }
        public ulong SubSystem()
        {
            return (ulong)asmbly.NTHeader.OptionalHeader.SubSystem;
        }
        public bool CanLoad(string FName, out string descr)
        {
            descr = "TUPLoader";
            Win32Assembly asmbl = Win32Assembly.LoadFile(FName);
            return (asmbl.NTHeader.Signature == ImageSignature.NT);
        }
        public IntPtr LoadFile(string FName)
        {
            this.FName = FName;
            asmbly = Win32Assembly.LoadFile(FName);
            return IntPtr.Zero;
        }
    }
}
