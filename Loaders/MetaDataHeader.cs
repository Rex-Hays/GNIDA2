using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNIDA.Loaders
{
    /// <summary>
    /// Represents a MetaData Header from a .NET Header of a .NET application.
    /// </summary>
    public class MetaDataHeader : IHeader
    {
        internal NETHeaderReader _reader;
        internal MetaDataHeader(NETHeaderReader reader)
        {
            this._reader = reader;

        }
        /// <summary>
        /// Gets the version string of the metadata.
        /// </summary>
        public string VersionString
        {
            get { return _reader.metadataVersionString; }
        }


        /// <summary>
        /// Gets the parent assembly container of the header.
        /// </summary>
        public LWin32 ParentAssembly
        {
            get
            {
                return null;
            }
        }
        /// <summary>
        /// Gets the raw file offset of the header.
        /// </summary>
        public long RawOffset
        {
            get
            {
                return _reader.metadataFileOffset;
            }
        }

    }
}
