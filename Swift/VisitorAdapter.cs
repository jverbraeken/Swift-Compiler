using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.Instructions;
using Swift.Instructions.Directives;
using Swift.AssTargets;

namespace Swift
{
    public class VisitorAdapter : Visitor
    {
        public virtual string visit(IntegerConstant n)
        {
            throw new NotImplementedException("IntegerConstant is not implemented yet");
            // Do nothing; leave the implementation to the main class
        }

        public virtual string visit(ByteConstant n)
        {
            throw new NotImplementedException("ByteConstant is not implemented yet");
            // Do nothing; leave the implementation to the main class
        }

        public virtual string visit(OctalConstant n)
        {
            throw new NotImplementedException("OctalConstant is not implemented yet");
            // Do nothing; leave the implementation to the main class
        }

        public virtual string visit(BinaryConstant n)
        {
            throw new NotImplementedException("BinaryConstant is not implemented yet");
            // Do nothing; leave the implementation to the main class
        }

        public virtual string visit(HexadecimalConstant n)
        {
            throw new NotImplementedException("HexadecimalConstant is not implemented yet");
            // Do nothing; leave the implementation to the main class
        }

        public virtual string visit(Register n)
        {
            throw new NotImplementedException("Register is not implemented yet");
            // Do nothing; leave the implementation to the main class
        }

        public virtual string visit(RegisterOffset n)
        {
            throw new NotImplementedException("RegisterOffset is not implemented yet");
            // Do nothing; leave the implementation to the main class
        }
        public virtual void visit(ConstDeclaration n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(VarDeclaration n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(BitwiseComplementExp n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(ExclamationExp n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(ModuloExp n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(OrExp powerExp)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Identifier identifier)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(PowerExp n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(StringLiteral n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Int8Type t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Call n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Leave n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Nope n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Push n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Sub n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(File n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(SectionCode n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(MakeGlobal n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Debug n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Ret n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Pop n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Move n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Label n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Comment n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Add n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(IdentifierType id)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(CharType t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(FloatType t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Int32Type t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(StringType t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(UInt16Type t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(UInt64Type t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(VoidType t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(UInt32Type t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(UInt8Type t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Int64Type t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Int16Type t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(DoubleType t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(BooleanType t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(IntegerLiteral n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(PlusExp n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(IdentifierExp identifier)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(MultiplicationExp n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(MinusExp n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(DivisionExp n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(AndExp n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(FunctionCall n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Base n)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(Assignment n)
        {
            // Do nothing; leave the implementation to the main class
        }
    }
}
