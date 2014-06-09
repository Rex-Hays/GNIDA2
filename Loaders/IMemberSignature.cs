using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GNIDA.Loaders
{
    public interface IMemberSignature
    {
        TypeReference ReturnType { get; }
    }
}
