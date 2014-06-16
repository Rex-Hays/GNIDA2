using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GNIDA.Loaders;
using GNIDA.Loaders;
using plugins;

namespace LoaderWin32
{
	public class LoaderWin32 : /*LWin32, */ILoader 
    {
        LWin32 ldr;
		public ExecutableFlags ExecutableFlags()
		{
            return ldr.NTHeader.FileHeader.ExecutableFlags;
		}
        public ulong ImageBase()
        {
            return ldr.NTHeader.OptionalHeader.ImageBase;
        }
		public bool CanLoad(string FName)
		{
            ldr = LWin32.LoadFile(FName);
            return (ldr.NTHeader.Signature == ImageSignature.NT);
		}
    }
}
