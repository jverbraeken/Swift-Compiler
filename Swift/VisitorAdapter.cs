using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.Instructions;
using Swift.Instructions.Directives;

namespace Swift
{
    public class VisitorAdapter : Visitor
    {
        public virtual void visit(ConstDeclaration n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(VarDeclaration n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(BitwiseComplementExp n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(ExclamationExp n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(ModuloExp n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(OrExp powerExp)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Identifier identifier)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(PowerExp n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(StringLiteral n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(IntegerType t)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Call n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Instruction n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Leave n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Nope n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Push n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Sub n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(File n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(SectionCode n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(MakeGlobal n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Debug n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Ret n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Pop n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Move n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Label n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Comment n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Add n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(IdentifierType id)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(IntegerLiteral n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(PlusExp n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(IdentifierExp identifier)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(MultiplicationExp n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(MinusExp n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(DivisionExp n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(AndExp n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(FunctionCall n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Base n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(Assignment n)
        {
            throw new NotImplementedException();
        }
    }
}
