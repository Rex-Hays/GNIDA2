using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNIDA.Loaders
{
    /// <summary>
    /// Represents the header of a Portable Executeable file
    /// </summary>
    public class NTHeader : IHeader
    {
        internal PeHeaderReader _header;
        internal FileHeader _fheader;
        internal string _file;
        internal LWin32 _assembly;

        internal NTHeader()
        {

        }

        /// <summary>
        /// Gets the file destination the header is located in.
        /// </summary>
        public string FilePath
        {
            get { return _file; }
        }
        /// <summary>
        /// Gets the File Header representation of the NT Header.
        /// </summary>
        public FileHeader FileHeader
        {
            get
            {
                return _fheader;
            }
        }
        /// <summary>
        /// Gets the Optional Header representation of the NT Header.
        /// </summary>
        public IOptionalHeader OptionalHeader
        {
            get
            {
                return _header.optionalHeader;
            }
        }
        /// <summary>
        /// Gets the image signature of the portable executable.
        /// </summary>
        public ImageSignature Signature
        {
            get
            {
                return (ImageSignature)_header.ntHeadersSignature;
            }
        }
        /// <summary>
        /// Returns true if the loaded pe file is managed and uses a .NET Runtime.
        /// </summary>
        /// <returns></returns>
        public bool IsManagedAssembly
        {
            get { return _header.datadirectories.Count >= (int)DataDirectoryName.Clr && _header.datadirectories[(int)DataDirectoryName.Clr].Size > 0; }
        }
        /// <summary>
        /// Gets the sections of the portable executable file.
        /// </summary>
        public List<Section> Sections
        {
            get { return _header.sections; }
        }
        /// <summary>
        /// Gets the parent assembly container of the NT header.
        /// </summary>
        public LWin32 ParentAssembly
        {
            get
            {
                return _assembly;
            }
        }
        /// <summary>
        /// Gets the raw file offset of the header.
        /// </summary>
        public long RawOffset
        {
            get
            {
                return _header.ntheaderoffset;
            }
        }

        /// <summary>
        /// Gets the Portable Executeable's NT header by specifing the assembly.
        /// </summary>
        /// <param name="assembly">The assembly to read the nt header</param>
        /// <returns></returns>
        public static NTHeader FromAssembly(LWin32 assembly)
        {
            NTHeader a = new NTHeader();
            a._assembly = assembly;
            a._file = assembly._path;
            a._header = assembly._headerReader;
            a._fheader = FileHeader.FromAssembly(assembly);
            return a;
        }
    }
}
