using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using plugins;
using TUP;
using TUP.AsmResolver;

namespace DasmerTUP
{
    public class Instr : IInstruction
    {
        public TUP.AsmResolver.ASM.x86Instruction ins;
        public Instr()
        {
            ops = new OPERAND[3];//OPERAND[3];
        }

        private static string AddProc(ulong x, MyDictionary ProcList, Dictionary<ulong, TFunc> NewSubs)
        {
            if (ProcList.ContainsKey(x)) return ProcList[x].FName + "();";
            TFunc tmpfunc = new TFunc(x, 1);
            if (!NewSubs.ContainsKey(x)) NewSubs.Add(x, tmpfunc);
            return "proc_" + x.ToString("X8") + "();";
        }
        public override string ToString(MyDictionary ProcList, VarDictionary VarDict, Dictionary<ulong, TFunc> NewSubs)
        {
            string s = "$"+ins.ToAsmString();
            if (bytes[0] == 0xE8)
                if (ins.Operand1.ValueType == TUP.AsmResolver.ASM.OperandType.Normal)
                    s = AddProc(((Offset)ins.Operand1.Value).FileOffset + Addr, ProcList, NewSubs);
            if (bytes[0] == 0xFF)
                if (bytes[1] == 0x15)
                    if (ins.Operand1.ValueType == TUP.AsmResolver.ASM.OperandType.DwordPointer)
                        s = AddProc(((Offset)ins.Operand1.Value).Va, ProcList, NewSubs);

            if (bytes[0] == 0xA3)//mov somevar, EAX
            {
                TVar Var1 = new TVar(((Offset)ins.Operand1.Value).Va, "", 4);
                if (!VarDict.ContainsKey(((Offset)ins.Operand1.Value).Va))
                {
                    VarDict.AddVar(Var1);
                };
                s = VarDict[((Offset)ins.Operand1.Value).Va].FName + " = EAX;";
            }
            if ((bytes[0] == 0xC2) |//retn
                (bytes[0] == 0xC3))//ret
                s = "$ret";
                return s;
        }
    }
    public class DasmerTUP : IDasmer
    {
        public string Name(){ return "TUP.Dasmer"; }
        public static ILoader assembly;
        TUP.AsmResolver.ASM.x86Disassembler dsm;
        public void Init(ILoader _assembly)
        {
            assembly = _assembly;
            //Win32Assembly asm = Win32Assembly.LoadFile(assembly.FName);
        }
        public UInt32 disassemble(ulong offset, out IInstruction instr, ref DISASM_INOUT_PARAMS param)
        {
            byte[] bt = assembly.ReadBytes(offset, 10);
            dsm = new TUP.AsmResolver.ASM.x86Disassembler(bt);
            dsm.CurrentOffset = 0;
            Instr instr1 = new Instr();
            instr1.Addr = offset;
            instr1.ins = dsm.DisassembleNextInstruction();
            instr1.bytes = assembly.ReadBytes(offset, instr1.ins.Size);

            if (instr1.bytes[0] == 0xFF)
                if (instr1.bytes[1] == 0x15)
                    if (instr1.ins.Operand1 != null)
                        instr1.disp.value.d64 = ((Offset)instr1.ins.Operand1.Value).Va;//Call ExitProcess probably
            
            instr = instr1;
            return (UInt32)instr1.ins.Size;
        }
    }
}
