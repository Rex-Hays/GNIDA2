using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNIDA.Loaders
{
    interface ILoader
    {
         bool CanLoad(string FName);
    }
}
