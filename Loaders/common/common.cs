using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ldrs
{
    public interface ILoader
    {
        bool CanLoad(string FName);
    }
    public class Loader : ILoader
    {
        public bool CanLoad(string FName)
        {
            return false;
        }
    }
}
