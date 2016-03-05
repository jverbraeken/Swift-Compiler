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
    class X86_64 : InstructionSetGenerator
    {
        private const int WORD_SIZE = 8;
        public X86_64(System.IO.StreamWriter stream) : base(stream)
        {
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
            w(".def\t" + n.ToString());
        }

        public override void visit(MakeGlobal n)
        {
            w(".globl\t" + n.Method);
        }

        public override void visit(SectionCode n)
        {
            w(".text");
        }

        public override void visit(File n)
        {
            w(".file\t\"" + n.Name + "\"");
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



        public override string visit(Register n)
        {
            switch (n.Value)
            {
                case Global.Registers.BASEPOINTER: return "%rbp";
                case Global.Registers.RAX: return "%rax";
                case Global.Registers.RDX: return "%rdx";
                case Global.Registers.STACKPOINTER: return "%rsp";
            }
            return null;
        }

        public override string visit(RegisterOffset n)
        {
            switch (n.Value)
            {
                case Global.Registers.BASEPOINTER: return "-" + n.Offset * 8 + "(%rbp)";
                case Global.Registers.RAX: return "-" + n.Offset * 8 + "(%rax)";
                case Global.Registers.RDX: return "-" + n.Offset * 8 + "(%rdx)";
                case Global.Registers.STACKPOINTER: return "-" + n.Offset * 8 + "(%rsp)";
            }
            return null;
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
            //return "$" + n.Value.ToString();
            return null;
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
