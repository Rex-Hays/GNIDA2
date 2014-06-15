using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GNIDA.Loaders;
using ldrs;
using System.Runtime.InteropServices;

namespace bfdLoader
{
    public class bfdLoader// : ILoader 
    {
        public struct bfd
        {
        };
        [DllImport("libbfd.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern bfd bfd_open(String filename, String target);

        //[DllImport("user32.dll", CharSet = CharSet.Unicode)]
        //public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
        public bool CanLoad(string FName)
        {
            bfd_open(FName, null);
            //MessageBox(IntPtr.Zero, "123", "321", 0);
            return true;
        }
    }
}
