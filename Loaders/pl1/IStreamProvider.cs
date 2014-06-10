using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GNIDA.Loaders
{
    public interface IStreamProvider : IMetaDataMember
    {
        Stream Stream { get; }
    }
}
