using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNIDA.Loaders;

namespace LoaderWin32
{
	public class LoaderWin32 : /*LWin32, */ILoader 
    {
        LWin32 ldr;
		public bool CanLoad(string FName)
		{
            ldr = LWin32.LoadFile(FName);
            return (ldr.NTHeader.Signature == ImageSignature.NT);
		}
    }
}
