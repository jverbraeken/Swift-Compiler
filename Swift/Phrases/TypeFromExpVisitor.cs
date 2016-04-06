using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swift.Symbols;

namespace Swift.Phrases
{
    public class TypeFromExpVisitor// : TypeVisitor
    {
        /*public ASTType type { get; set; }

        public TypeFromExpVisitor()
        {
        }

        public ASTType visit(PlusExp n)
        {
            ASTType t1 = n.e1.accept(this);
            ASTType t2 = n.e2.accept(this);
            if (t1.Equals(t2))
                return t1;
            Swift.error("The types of the terms of the expression on line " + n.Context.GetLine() + ", column " + n.Context.GetPos() + " don't match", 1);
            return null;
        }















        public ASTType visit(IntegerLiteral n)
        {
            switch (Swift.architecture)
            {
                case Global.InstructionSets.X86: return new Int32Type();
                case Global.InstructionSets.X86_64: return new Int64Type();
                default: return null;
            }
        }

        public ASTType visit(StringLiteral n)
        {
            return new StringType();
        }

        public ASTType visit(IdentifierExp n)
        {
            return n.Scope.Lookup(n.ID.Name).accept(this);
        }

        public ASTType visit(BuiltinFunctionSymbol e)
        {
            return e.ReturnTypes[0].Type;
        }

        public ASTType visit(ConstantSymbol e)
        {
            return e.Type;
        }

        public ASTType visit(FunctionSymbol e)
        {
            return e.ReturnTypes[0].Type;
        }

        public ASTType visit(VariableSymbol e)
        {
            return e.Type;
        }

        public ASTType visit(AndExp e)
        {
            throw new NotImplementedException();
        }

        public ASTType visit(BitwiseNotExp e)
        {
            throw new NotImplementedException();
        }

        public ASTType visit(DivisionExp e)
        {
            throw new NotImplementedException();
        }

        public ASTType visit(ExclamationExp e)
        {
            throw new NotImplementedException();
        }

        public ASTType visit(MinusExp e)
        {
            throw new NotImplementedException();
        }

        public ASTType visit(ModuloExp e)
        {
            throw new NotImplementedException();
        }

        public ASTType visit(MultiplicationExp e)
        {
            throw new NotImplementedException();
        }

        public ASTType visit(OrExp e)
        {
            throw new NotImplementedException();
        }

        public ASTType visit(PowerExp e)
        {
            throw new NotImplementedException();
        }*/
    }
}