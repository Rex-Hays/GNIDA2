using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNIDA.Loaders;

namespace LoaderWin32
{
	public class LoaderWin32 : ILoader
    {
		public bool CanLoad(string FName)
		{
			return true;
		}
    }
}
