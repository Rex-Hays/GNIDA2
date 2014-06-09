using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GNIDA.Loaders
{
    public class FieldSignature : IMemberSignature
    {
        public TypeReference ReturnType
        {
            get;
            internal set;
        }

    }
}
