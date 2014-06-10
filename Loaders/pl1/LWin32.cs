using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GNIDA.Loaders
{
    public class LWin32
    {
        internal string _path;
        internal PeImage _peImage;
        internal NTHeader _ntHeader;
        internal MZHeader _mzHeader;
        internal NETHeader _netHeader;
        internal PeHeaderReader _headerReader;
        internal ImportExportTableReader _importExportTableReader;
        internal ResourcesReader _resourcesReader;
        public PeImage Image
        {
            get { return _peImage; }
        }
        /// <summary>
        /// Gets the .NET header (if available) of the loaded portable executable file. 
        /// </summary>
        public NETHeader NETHeader
        {
            get { return _netHeader; }
        }
        public NTHeader NTHeader
        {
            get { return _ntHeader; }
        }
        /// <summary>
        /// Gets the imported methods of the Win32 Assembly
        /// </summary>
        public List<LibraryReference> LibraryImports
        {
            get
            {
                if (_importExportTableReader != null)
                    return _importExportTableReader.Imports;
                return new List<LibraryReference>();
            }
        }
        /// <summary>
        /// Gets the exports of the Win32 Assembly
        /// </summary>
        public List<ExportMethod> LibraryExports
        {
            get
            {
                if (_importExportTableReader != null)
                    return _importExportTableReader.Exports;
                return new List<ExportMethod>();
            }
        }
        public string Path
        {
            get
            {
                return _path;
            }
        }
        /// <summary>
        /// Gets the reading arguments that are being used to open the application.
        /// </summary>
        public ReadingParameters ReadingArguments { get; private set; }
        public static LWin32 LoadFile(string file)
        {
            return LoadFile(file, new ReadingParameters());
        }
        /// <summary>
        /// Loads an assembly from a specific file using the specific reading parameters.
        /// </summary>
        /// <param name="file">The file to read.</param>
        /// <param name="arguments">The reading parameters to use.</param>
        /// <returns></returns>
        /// <exception cref="System.BadImageFormatException"></exception>
        public static LWin32 LoadFile(string file, ReadingParameters arguments)
        {
            try
            {
                LWin32 a = new LWin32();
                a._path = file;
                a.ReadingArguments = arguments;
                a._peImage = PeImage.LoadFromAssembly(a);

                a._headerReader = PeHeaderReader.FromAssembly(a);
                a._ntHeader = NTHeader.FromAssembly(a);
                a._mzHeader = MZHeader.FromAssembly(a);
                a._headerReader.LoadData(arguments.IgnoreDataDirectoryAmount);


                if (!arguments.OnlyManaged)
                {
                    a._importExportTableReader = new ImportExportTableReader(a._ntHeader);
                    a._resourcesReader = new ResourcesReader(a._ntHeader);
                }


                a._netHeader = NETHeader.FromAssembly(a);
                a._peImage.SetOffset(a._ntHeader.OptionalHeader.HeaderSize);
                return a;
            }
            catch (Exception ex)
            {
                if (ex is AccessViolationException || ex is FileNotFoundException)
                    throw;
                throw new BadImageFormatException("The file is not a valid Portable Executable File.", ex);
            }
        }
    }
}
