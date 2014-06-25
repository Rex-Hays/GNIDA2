using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNIDA;
using GNIDA.Loaders;
//using medi;
using System.Runtime.InteropServices;

namespace plugins
{
    public class ImportMethods1
    {
        public ulong RVA;
        public ulong Ordinal;
        public string Name;
        public ImportMethods1(ulong _RVA, ulong _Ordinal, string _Name)
        {
            RVA = _RVA;
            Ordinal = _Ordinal;
            Name = _Name;
        }
    }
    public class LibraryReference1
    {
        public string LibraryName;
        public ImportMethods1[] ImportMethods;
        public LibraryReference1(string _LibraryName)
        {
            LibraryName = _LibraryName;
        }
    }
    public class ExportMethod1
    {
        public ulong RVA;
        public ulong Ordinal;
        public string Name;
        public ExportMethod1(ulong _RVA, ulong _Ordinal, string _Name)
        {
            RVA = _RVA;
            Ordinal = _Ordinal;
            Name = _Name;
        }
    }
    public class Section1
    {
        public ulong RVA;
        public ulong VirtualSize;
        public string Name;
        public ulong RawOffset;
        ulong SizeOfRawData;
        public ulong RawSize() { return SizeOfRawData; }
        public bool ContainsRawOffset(ulong rawoffset)
        {
            return ((rawoffset >= this.RawOffset) & (rawoffset < (this.RawOffset + this.RawSize())));
        }
        public bool ContainsRva(ulong Rva)
        {
            ulong endoffset = this.RVA + this.VirtualSize;
            return ((Rva >= this.RVA) & (Rva <= endoffset));
        }
        public ulong RVAToFileOffset(ulong rva)
        {
            return rva - this.RVA + RawOffset;
        }
        public Section1(ulong _RVA, ulong _VirtualSize, string _Name, ulong _RawOffset, ulong _SizeOfRawData)
        {
            RVA = _RVA;
            VirtualSize = _VirtualSize;
            Name = _Name;
            RawOffset = _RawOffset;
            SizeOfRawData = _SizeOfRawData;
        }
    }
    //[StructLayout(LayoutKind.Explicit)]
    public struct DISPLACEMENT
    {
        //[FieldOffset(0)]
        public byte size;
        //[FieldOffset(1)]
        public byte offset;
        //[FieldOffset(2)]
        public value2 value;
        [StructLayout(LayoutKind.Explicit)]
        public struct value2
        {
            [FieldOffset(0)]
            public UInt16 d16;
            [FieldOffset(0)]
            public UInt32 d32;
            [FieldOffset(0)]
            public UInt64 d64;
            //[FieldOffset(0)]
            //public byte[] ab;
        }
    };
    public class TFunc
    {
        public ulong Addr;
        public ulong Length;
        public byte[] bytes;
        public string FName;
        public string LibraryName;
        public uint type;
        public ulong Ordinal;
        public TFunc(ulong addr, uint Type, ulong Ord = 0, string Name = "", string LibName = "")
        {
            Addr = addr;
            type = Type;
            Ordinal = Ord;
            LibraryName = LibName;
            if (Name != "") FName = Name;
            else FName = "proc_" + Addr.ToString("X8");
        }
    }
    public class MyDictionary : Dictionary<ulong, TFunc>
    {
        public IGNIDA Parent;
        public void AddFunc(TFunc value)
        {
            if (!this.ContainsKey(value.Addr))
            {
                this.Add(value.Addr, value);
                Parent.RaiseAddFuncEvent(this, value);
                if (value.type != 3)
                    if ((!Parent.ToDisasmFuncList().ContainsKey(value.Addr))
                      || (!Parent.DisasmedFuncList().ContainsKey(value.Addr))) Parent.ToDisasmFuncList().Add(value.Addr, value);
            }
        }
    }

    public class TVar
    {
        public ulong Addr;
        public string FName;
        public uint type;
        public string ToStr()
        {
            switch (type)
            {
                case 0: return "void " + FName;
                case 1: return "byte " + FName;
                case 2: return "word " + FName;
                case 4: return "dword " + FName;
                default: return "void " + FName;
            }
        }
        public TVar(ulong addr, string Name = "", uint Type = 0)
        {
            Addr = addr;
            type = Type;
            if (Name != "") FName = Name;
            else
                switch (Type)
                {
                    case 1: FName = "byte_" + Addr.ToString("X8"); break;
                    case 2: FName = "word_" + Addr.ToString("X8"); break;
                    case 4: FName = "dword_" + Addr.ToString("X8"); break;
                    default: FName = "unk_" + Addr.ToString("X8"); break;
                }
        }
    }
    public class VarDictionary : Dictionary<ulong, TVar>
    {
        public IGNIDA Parent;
        public void AddVar(TVar value)
        {
            if (!this.ContainsKey(value.Addr))
            {
                this.Add(value.Addr, value);
                Parent.RaiseVarFuncEvent(this, value);
            }
        }
    }

    public enum REG_TYPE
    {
        REG_TYPE_GEN = 0x0,
        REG_TYPE_SEG = 0x1,
        REG_TYPE_CR = 0x2,
        REG_TYPE_DBG = 0x3,
        REG_TYPE_TR = 0x4,
        REG_TYPE_FPU = 0x5,
        REG_TYPE_MMX = 0x7,
        REG_TYPE_XMM = 0x8
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct reg1
    {
        [FieldOffset(0)]
        public byte code;
        [FieldOffset(1)]
        public REG_TYPE type;
    };
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct far_addr321
    {
        [FieldOffset(0)]
        public UInt16 offset;
        [FieldOffset(2)]
        public UInt16 seg;
        //[FieldOffset(0)]
        //public UInt32 Val;
    }
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct imm1
    {
        [FieldOffset(0)]
        public byte imm8;
        [FieldOffset(0)]
        public UInt16 imm16;
        [FieldOffset(0)]
        public UInt32 imm32;
        [FieldOffset(0)]
        public UInt64 imm64;
        //[FieldOffset(0)]
        //public byte[] immab;
        [FieldOffset(8)]
        public byte size;
        [FieldOffset(9)]
        public byte offset;
    }
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct far_addr481
    {
        [FieldOffset(0)]
        public UInt32 offset;
        [FieldOffset(4)]
        public UInt16 seg;
        [FieldOffset(0)]
        public UInt64 Val;
    }
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct far_addr1
    {
        [FieldOffset(0)]
        public far_addr321 far_addr32;
        [FieldOffset(0)]
        public far_addr481 far_addr48;
        //[FieldOffset(0)]
        //public byte[] far_addr_ab;
        [FieldOffset(6)]
        public byte offset;
    }
    public struct addr1
    {
        public byte seg;
        public byte mod;
        public byte bas;
        public byte index;
        public byte scale;
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct value1
    {
        [FieldOffset(0)]
        public reg1 reg;
        [FieldOffset(0)]
        public imm1 imm;
        [FieldOffset(0)]
        public far_addr1 far_addr;
        [FieldOffset(0)]
        public addr1 addr;
    }

    public struct OPERAND
    {
        public value1 value;
        public ushort size; //Fuck... I need 16_t only for 'stx' size qualifier.
        public byte flags;
    };

    public abstract class IInstruction
    {
        public byte[] bytes { get; set; }
        public ulong Addr { get; set; }
        public abstract string ToString(MyDictionary ProcList, VarDictionary VarDict, Dictionary<ulong, TFunc> NewSubs);
        public string mnemonic { get; set; }
        public OPERAND[] ops;//OPERAND[3];
        public DISPLACEMENT disp;
    }
    public interface IPlugin
    {
        string Description();

    }
    public interface IGNIDA
    {
        MyDictionary ToDisasmFuncList();
        MyDictionary DisasmedFuncList();
        void RaiseAddFuncEvent(object sender, TFunc Func);
        void RaiseVarFuncEvent(object sender, TVar Var);
    }
    public interface ILoader
    {
        string FName { get; set; }
        ulong SubSystem();
        ulong ExecutableFlags();
        ulong ImageBase();
        ulong Entrypoint();
        List<Section1> Sections();
        List<ExportMethod1> LibraryExports();
        List<LibraryReference1> LibraryImports();
        byte[] ReadBytes(ulong offset, int length);
        bool CanLoad(string FName, out string descr);
        IntPtr LoadFile(string FName);
    }
    public enum ERRS
    {
        ERR_OK,
        ERR_BADCODE,
        ERR_TOO_LONG,
        ERR_NON_LOCKABLE,
        ERR_RM_REG,
        ERR_RM_MEM,
        ERR_16_32_ONLY,
        ERR_64_ONLY,
        ERR_REX_NOOPCD,
        ERR_ANOT_ARCH,
        ERR_INTERNAL
    };
    public enum DISMODE
    {
        DISASSEMBLE_MODE_16 = 0x1,
        DISASSEMBLE_MODE_32 = 0x2,
        DISASSEMBLE_MODE_64 = 0x4,
    }
    public struct DISASM_INOUT_PARAMS
    {
        public int sf_prefixes_len;
        public byte[] sf_prefixes;
        public ERRS errcode;
        public byte arch;
        public DISMODE mode;
        public byte options;
        public UInt64 bas;
    };
    public static class Dasmer
    {
        public static uint MAX_MNEMONIC_LEN = 0x0C;
        public static uint MAX_INSTRUCTION_LEN = 0x0F;
        //DISASM_INOUT_PARAMS.options' bits:
        public static byte DISASM_OPTION_APPLY_REL = 0x1;
        public static byte DISASM_OPTION_OPTIMIZE_DISP = 0x2;
        public static byte ARCH_COMMON = 0x1;
        public static byte ARCH_INTEL = 0x2;
        public static byte ARCH_AMD = 0x4;
        public static byte ARCH_ALL = (byte)((int)ARCH_COMMON | (int)ARCH_INTEL | (int)ARCH_AMD);
    }
    public interface IDasmer
   {
       void Init(ILoader ldr);
       string Name();
        UInt32 disassemble(ulong offset, out IInstruction instr, ref DISASM_INOUT_PARAMS param);
    }
}
