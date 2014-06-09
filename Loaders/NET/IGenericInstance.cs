using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNIDA.Loaders
{
    public interface IGenericInstance : IMetaDataMember
    {
        TypeReference[] GenericArguments { get; }
        bool HasGenericArguments { get; }
    }
}
