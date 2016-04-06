using Swift.AssTargets;
using Swift.Instructions;
using Swift.Instructions.Directives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Phrases
{
    public static class ModuleListComparer
    {
        public static bool Compare(List<Module> x, List<Module> y)
        {
            if (x.Count != y.Count)
                return false;
            for (int i = 0; i < x.Count; i++)
                if (!AreEqual(x[i], y[i]))
                    return false;
            return true;
        }

        public static bool AreEqual(Module x, Module y)
        {
            if (x.InterCode.Count != y.InterCode.Count || x.StringTable.Count != y.StringTable.Count)
                return false;
            for (int i = 0; i < x.InterCode.Count; i++)
                if (!AreEqual(x.InterCode[i], y.InterCode[i]))
                    return false;
            foreach (KeyValuePair<string, string> entry in x.StringTable)
                if (entry.Value != y.StringTable[entry.Key])
                    return false;
            return true;
        }

        public static bool AreEqual(Instruction x, Instruction y)
        {
            if (x.GetType() != y.GetType())
                return false;
            switch (MethodName(x.GetType().ToString()))
            {
                case "Debug": return (((Debug)x).Info == ((Debug)y).Info);
                case "MakeGlobal": return (((MakeGlobal)x).Method == ((MakeGlobal)y).Method);
                case "SectionCode": return true;
                case "Add": return (AreEqual(((Add)x).Target, ((Add)y).Target) && AreEqual(((Add)x).Value, ((Add)y).Value));
                case "And": return (AreEqual(((And)x).Val1, ((And)y).Val1) && AreEqual(((And)x).Val2, ((And)y).Val2));
                case "Call": return (((Call)x).Name == ((Call)y).Name);
                case "Comment": return (((Comment)x).Name == ((Comment)y).Name);
                case "Compare": return (AreEqual(((Compare)x).Val1, ((Compare)y).Val1) && AreEqual(((Compare)x).Val2, ((Compare)y).Val2));
                case "Jump": return (((Jump)x).Name == ((Jump)y).Name);
                case "JumpE": return (((JumpE)x).Name == ((JumpE)y).Name);
                case "JumpG": return (((JumpG)x).Name == ((JumpG)y).Name);
                case "JumpGE": return (((JumpGE)x).Name == ((JumpGE)y).Name);
                case "JumpL": return (((JumpL)x).Name == ((JumpL)y).Name);
                case "JumpLE": return (((Jump)x).Name == ((JumpLE)y).Name);
                case "JumpNE": return (((JumpNE)x).Name == ((JumpNE)y).Name);
                case "Label": return (((Label)x).Method == ((Label)y).Method);
                case "Lea": return (AreEqual(((Lea)x).From, ((Lea)y).From) && AreEqual(((Lea)x).To, ((Lea)y).To));
                case "Leave": return true;
                case "Move": return (AreEqual(((Move)x).From, ((Move)y).From) && AreEqual(((Move)x).To, ((Move)y).To));
                case "Nope": return true;
                case "Or": return (AreEqual(((Or)x).Val1, ((Or)y).Val1) && AreEqual(((Or)x).Val2, ((Or)y).Val2));
                case "Pop": return (AreEqual(((Pop)x).Target, ((Pop)y).Target));
                case "Push": return (AreEqual(((Push)x).Target, ((Push)y).Target));
                case "Ret": return true;
                case "Shl": return (AreEqual(((Shl)x).Target, ((Shl)y).Target) && AreEqual(((Shl)x).Value, ((Shl)y).Value));
                case "Shr": return (AreEqual(((Shr)x).Target, ((Shr)y).Target) && AreEqual(((Shr)x).Value, ((Shr)y).Value));
                case "Sub": return (AreEqual(((Sub)x).Target, ((Sub)y).Target) && AreEqual(((Sub)x).Value, ((Sub)y).Value));
                case "Xchg": return (AreEqual(((Xchg)x).Val1, ((Xchg)y).Val1) && AreEqual(((Xchg)x).Val2, ((Xchg)y).Val2));
                case "Xor": return (AreEqual(((Xor)x).Val1, ((Xor)y).Val1) && AreEqual(((Xor)x).Val2, ((Xor)y).Val2));
                default: return false;
            }
        }

        public static bool AreEqual(AssTarget x, AssTarget y)
        {
            if (x.GetType() != y.GetType())
                return false;
            switch (MethodName(x.GetType().ToString()))
            {
                case "IntegerConstant": return ((IntegerConstant)x).Value == ((IntegerConstant)y).Value;
                case "BinaryConstant": return ((BinaryConstant)x).Value == ((BinaryConstant)y).Value;
                case "ByteConstant": return ((ByteConstant)x).Value == ((ByteConstant)y).Value;
                case "HexadecimalConstant": return ((HexadecimalConstant)x).Value == ((HexadecimalConstant)y).Value;
                case "OctalConstant": return ((OctalConstant)x).Value == ((OctalConstant)y).Value;
                case "ParamRegister": return ((ParamRegister)x).Position == ((ParamRegister)y).Position;
                case "Register": return ((Register)x).Value == ((Register)y).Value;
                case "RegisterOffset": return ((RegisterOffset)x).Register == ((RegisterOffset)y).Register && ((RegisterOffset)x).IntOffset == ((RegisterOffset)y).IntOffset && ((RegisterOffset)x).LabelOffset == ((RegisterOffset)y).LabelOffset;
                default: return false;
            }
        }

        private static string MethodName(string name)
        {
            return name.Substring(name.LastIndexOf(".") + 1);
        }
    }
}
