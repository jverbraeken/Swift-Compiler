using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swift.AssTargets;
using Swift.Instructions;
using Swift.Instructions.Directives;

namespace Swift.InstructionSetGenerators
{
    public class X86_64 : InstructionSetGenerator
    {
        private const int WORD_SIZE = 8;
        public X86_64(System.IO.StreamWriter stream) : base(stream)
        {
        }

        public override void processModules(string original_file, List<Module> modules)
        {
            w(".file\t\"" + original_file + "\"");
            foreach (Module module in modules)
                processModule(module);
            w(".ident\t\"Yontu: (x86_64-posix-seh-rev0, Built by Joost Verbraeken) BETA\"");
        }

        public void processModule(Module module)
        {
            if (module.StringTable.Count > 0)
                w(".section\t.rdata,\"dr\"");
            foreach (KeyValuePair<string, string> entry in module.StringTable)
            {
                wn(entry.Value + ":");
                w(".asciz\t\"" + entry.Key + "\"");
            }

            foreach (Instruction instruction in module.InterCode)
                instruction.accept(this);
        }

        private string getRegisterName(Global.Registers n)
        {
            switch (n)
            {
                case Global.Registers.STACKBASEPOINTER: return "%rbp";
                case Global.Registers.STACKPOINTER: return "%rsp";
                case Global.Registers.ACCUMULATOR: return "%rax";
                case Global.Registers.BASE: return "%rbx";
                case Global.Registers.COUNTER: return "%rcx";
                case Global.Registers.DATA: return "%rdx";
                case Global.Registers.INSTRUCTIONPOINTER: return "%rip";
                case Global.Registers.DEST_INDEX: return "%rdi";
                case Global.Registers.SRC_INDEX: return "%rsi";
                default: return null;
            }
        }

        public override void visit(Comment n)
        {
            w(".ident\t\"" + n.Name + "\"");
        }

        public override void visit(Label n)
        {
            wn(n.Method + ":");
        }

        public override void visit(Move n)
        {
            w("movq\t" + n.From.accept(this) + ", " + n.To.accept(this));
        }

        public override void visit(Pop n)
        {
            w("popq\t" + n.Target.accept(this));
        }

        public override void visit(Ret n)
        {
            w("ret");
        }

        public override void visit(Debug n)
        {
            w(".def\t" + n.Info);
        }

        public override void visit(MakeGlobal n)
        {
            w(".globl\t" + n.Method);
        }

        public override void visit(SectionCode n)
        {
            w(".text");
        }

        public override void visit(Sub n)
        {
            w("subq\t" + n.Value.accept(this) + ", " + n.Target.accept(this));
        }

        public override void visit(Push n)
        {
            w("pushq\t" + n.Target.accept(this));
        }

        public override void visit(Nope n)
        {
            w("nop");
        }

        public override void visit(Lea n)
        {
            w("leaq\t" + n.From.accept(this) + ", " + n.To.accept(this));
        }

        public override void visit(Leave n)
        {
            w("leave");
        }

        public override void visit(Call n)
        {
            w("call\t" + n.Name);
        }

        public override void visit(Add n)
        {
            w("addq\t" + n.Value.accept(this) + ", " + n.Target.accept(this));
        }

        public override void visit(Mult n)
        {
            w("addq\t" + n.Value.accept(this) + ", " + n.Target.accept(this));
        }

        public override void visit(Divide n)
        {
            w("cltd");
            w("idivq\t" + n.Value.accept(this) + ", " + n.Target.accept(this));
        }



        public override string visit(ParamRegister n)
        {
            switch (n.Position)
            {
                case 0: return "%rcx";
                case 1: return "%rdx";
                case 2: return "%r8d";
                case 3: return "%r9d";
                default: return (n.Position + 1) * 8 + "(%rsp)";
            }
        }

        public override string visit(Register n)
        {
            switch (n.Value)
            {
                case Global.Registers.STACKBASEPOINTER: return "%rbp";
                case Global.Registers.ACCUMULATOR: return "%rax";
                case Global.Registers.BASE: return "%rbx";
                case Global.Registers.COUNTER: return "%rcx";
                case Global.Registers.DATA: return "%rdx";
                case Global.Registers.STACKPOINTER: return "%rsp";
                case Global.Registers.DEST_INDEX: return "%rdi";
                case Global.Registers.SRC_INDEX: return "%rsi";
                default: Swift.error("Internal error in swift: trying to access unexisting register", 1); break;
            }
            return null;
        }

        public override string visit(RegisterOffset n)
        {
            if (n.IntOffset == null)
            {
                return n.LabelOffset + "(" + getRegisterName(n.Register) + ")";
            }
            else {
                return n.IntOffset * 8 + "(" + getRegisterName(n.Register) + ")";
            }
        }

        public override string visit(IntegerConstant n)
        {
            return "$" + n.Value.ToString();
        }

        public override string visit(ByteConstant n)
        {
            return "$" + (n.Value * WORD_SIZE).ToString();
        }

        public override string visit(BinaryConstant n)
        {
            return "$" + Convert.ToInt64(n.Value.ToString(), 2).ToString();
        }

        public override string visit(OctalConstant n)
        {
            return "0" + n.Value.ToString();

        }

        public override string visit(HexadecimalConstant n)
        {
            return "0x" + n.Value.ToString();

        }
    }
}
