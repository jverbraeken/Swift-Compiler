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
        public X86_64(System.IO.StreamWriter stream) : base(stream)
        {
        }

        public override void visit(Comment n)
        {
            w(".ident\t" + n.Name);
        }

        public override void visit(Label n)
        {
            wn(n.Method + ";");
        }

        public override void visit(Move n)
        {
            throw new NotImplementedException();
        }

        public override void visit(Pop n)
        {
            switch (n.Target.GetType()) {
                case w("popq\t" + )
            }
        }

        public override void visit(Ret n)
        {
            throw new NotImplementedException();
        }

        public override void visit(Debug n)
        {
            throw new NotImplementedException();
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
            w(".file\t" + n.Name);
        }

        public override void visit(Sub n)
        {
            throw new NotImplementedException();
        }

        public override void visit(Push n)
        {
            throw new NotImplementedException();
        }

        public override void visit(Nope n)
        {
            throw new NotImplementedException();
        }

        public override void visit(Leave n)
        {
            throw new NotImplementedException();
        }

        public override void visit(Instruction n)
        {
            throw new NotImplementedException();
        }

        public override void visit(Call n)
        {
            throw new NotImplementedException();
        }

        public override void visit(Add n)
        {
            throw new NotImplementedException();
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
                case Global.Registers.BASEPOINTER: return "-" + n.Offset*8 + "(%rbp)";
                case Global.Registers.RAX: return "-" + n.Offset * 8 + "(%rax)";
                case Global.Registers.RDX: return "-" + n.Offset * 8 + "(%rdx)";
                case Global.Registers.STACKPOINTER: return "-" + n.Offset * 8 + "(%rsp)";
            }
            return null;
        }

        public override string visit(Constant n)
        {
            return n.V  
                    
        }
    }
}
