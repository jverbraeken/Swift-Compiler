using Swift.AssTargets;
using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.Instructions;
using Swift.Instructions.Directives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public interface Visitor
    {
        // AssTargets
        string visit(Constant n);
        string visit(Register n);
        string visit(RegisterOffset n);
        // AST Nodes
        void visit(Assignment n);
        void visit(Base n);
        void visit(ConstDeclaration n);
        void visit(FunctionCall n);
        void visit(VarDeclaration n);
        // AST Nodes - Expressions
        void visit(AndExp n);
        void visit(BitwiseComplementExp n);
        void visit(DivisionExp n);
        void visit(ExclamationExp n);
        void visit(MinusExp n);
        void visit(ModuloExp n);
        void visit(MultiplicationExp n);
        void visit(OrExp powerExp);
        void visit(IdentifierExp identifier);
        void visit(Identifier identifier);
        void visit(PlusExp n);
        void visit(PowerExp n);
        // AST Nodes - Expressions - Literals
        void visit(IntegerLiteral n);
        void visit(StringLiteral n);
        // AST Nodes - Types
        void visit(IdentifierType id);
        void visit(IntegerType t);
        // Instructions
        void visit(Add n);
        void visit(Call n);
        void visit(Comment n);
        void visit(Instruction n);
        void visit(Label n);
        void visit(Leave n);
        void visit(Move n);
        void visit(Nope n);
        void visit(Pop n);
        void visit(Push n);
        void visit(Ret n);
        void visit(Sub n);
        // Instructions - Directives
        void visit(Debug n);
        void visit(File n);
        void visit(MakeGlobal n);
        void visit(SectionCode n);
    }
}