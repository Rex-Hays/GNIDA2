using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNIDA.Loaders
{
    /// <summary>
    /// Represents a header of a portable executable file.
    /// </summary>
    public interface IHeader
    {


        /// <summary>
        /// Gets the parent assembly container of the header.
        /// </summary>
        LWin32 ParentAssembly { get; }
        /// <summary>
        /// Gets the raw file offset of the header.
        /// </summary>
        long RawOffset { get; }

    }
}
