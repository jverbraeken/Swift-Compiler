using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Phrases
{
    public class ScopeAssignVisitor : ASTVisitor
    {
        public Table Scope { get; set; }

        public override void visit(Assignment n)
        {
            n.Scope = Scope;
            n.RHS.accept(this);
        }

        public override void visit(TupleElement n)
        {
            n.Scope = Scope;
        }

        public override void visit(TupleElementList n)
        {
            n.Scope = Scope;
            foreach (ITupleParentElement element in n.List)
                element.Scope = Scope;
        }

        public override void visit(ParameterDeclaration n)
        {
            n.Scope = Scope;
            n.DefaultValue.accept(this);
        }

        public override void visit(ConstDeclaration n)
        {
            n.Scope = Scope;
            n.RHS.accept(this);
        }

        public override void visit(BitwiseNotExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(DivisionExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(MultiplicationExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(OverflowSubExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(BoolLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(OctalLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(Int32Literal n)
        {
            n.Scope = Scope;
        }

        public override void visit(UHexaLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(UInt32Literal n)
        {
            n.Scope = Scope;
        }

        public override void visit(UInt16Literal n)
        {
            n.Scope = Scope;
        }

        public override void visit(UOctalLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(Int16Literal n)
        {
            n.Scope = Scope;
        }

        public override void visit(BinaryLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(OverflowMultExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(ModuloExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(BitwiseXorExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(BitwiseLeftShiftExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(Base n)
        {
            //The base IS the global scope
        }

        public override void visit(AndExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(BitwiseOrExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(ExclamationExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(OrExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(DoubleLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(HexaLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(Int64Literal n)
        {
            n.Scope = Scope;
        }

        public override void visit(StringLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(UInt64Literal n)
        {
            n.Scope = Scope;
        }

        public override void visit(UInt8Literal n)
        {
            n.Scope = Scope;
        }

        public override void visit(UBinaryLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(Int8Literal n)
        {
            n.Scope = Scope;
        }

        public override void visit(FloatLiteral n)
        {
            n.Scope = Scope;
        }

        public override void visit(Identifier identifier)
        {
            throw new NotImplementedException();
        }

        public override void visit(OverflowAddExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(MinusExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(BitwiseRightShiftExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(BitwiseAndExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(FunctionCallExp n)
        {
            n.Scope = Scope;
            foreach (ParameterCall call in n.Args)
                call.Value.accept(this);
        }

        public override void visit(IdentifierExp n)
        {
            n.Scope = Scope;
        }

        public override void visit(PlusExp n)
        {
            n.Scope = Scope;
            n.e1.accept(this);
            n.e2.accept(this);
        }

        public override void visit(VarDeclaration n)
        {
            n.Scope = Scope;
        }
    }
}
