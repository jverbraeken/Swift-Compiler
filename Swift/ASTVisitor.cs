using Swift.AssTargets;
using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.Instructions;
using Swift.Instructions.Directives;
using Swift.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public abstract class ASTVisitor : Visitor
    {
        public abstract void visit(Assignment n);
        public abstract void visit(ConstDeclaration n);
        public abstract void visit(VarDeclaration n);
        public abstract void visit(BitwiseAndExp n);
        public abstract void visit(BitwiseNotExp n);
        public abstract void visit(BitwiseRightShiftExp n);
        public abstract void visit(DivisionExp n);
        public abstract void visit(MinusExp n);
        public abstract void visit(MultiplicationExp n);
        public abstract void visit(OverflowAddExp n);
        public abstract void visit(OverflowSubExp n);
        public abstract void visit(Identifier identifier);
        public abstract void visit(BoolLiteral n);
        public abstract void visit(FloatLiteral n);
        public abstract void visit(OctalLiteral n);
        public abstract void visit(Int8Literal n);
        public abstract void visit(Int32Literal n);
        public abstract void visit(UBinaryLiteral n);
        public abstract void visit(UHexaLiteral n);
        public abstract void visit(UInt8Literal n);
        public void visit(StringElement n)
        {
            throw new NotImplementedException();
        }

        public void visit(Literal n)
        {
            throw new NotImplementedException();
        }

        public string visit(IntegerConstant n)
        {
            throw new NotImplementedException();
        }

        public string visit(BinaryConstant n)
        {
            throw new NotImplementedException();
        }

        public string visit(HexadecimalConstant n)
        {
            throw new NotImplementedException();
        }

        public string visit(Register n)
        {
            throw new NotImplementedException();
        }

        public void visit(TypeCast t)
        {
            throw new NotImplementedException();
        }

        public void visit(BoolType t)
        {
            throw new NotImplementedException();
        }

        public void visit(DoubleType t)
        {
            throw new NotImplementedException();
        }

        public void visit(IdentifierType id)
        {
            throw new NotImplementedException();
        }

        public void visit(OctalType t)
        {
            throw new NotImplementedException();
        }

        public void visit(Int8Type t)
        {
            throw new NotImplementedException();
        }

        public void visit(Int32Type t)
        {
            throw new NotImplementedException();
        }

        public void visit(StringType t)
        {
            throw new NotImplementedException();
        }

        public void visit(UInt16Type t)
        {
            throw new NotImplementedException();
        }

        public void visit(UInt64Type t)
        {
            throw new NotImplementedException();
        }

        public void visit(Add n)
        {
            throw new NotImplementedException();
        }

        public void visit(Call n)
        {
            throw new NotImplementedException();
        }

        public void visit(Compare n)
        {
            throw new NotImplementedException();
        }

        public void visit(Jump n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpG n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpL n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpNE n)
        {
            throw new NotImplementedException();
        }

        public void visit(Lea n)
        {
            throw new NotImplementedException();
        }

        public void visit(Move n)
        {
            throw new NotImplementedException();
        }

        public void visit(Nope n)
        {
            throw new NotImplementedException();
        }

        public void visit(Pop n)
        {
            throw new NotImplementedException();
        }

        public void visit(Ret n)
        {
            throw new NotImplementedException();
        }

        public void visit(Shr n)
        {
            throw new NotImplementedException();
        }

        public void visit(Xchg n)
        {
            throw new NotImplementedException();
        }

        public void visit(Debug n)
        {
            throw new NotImplementedException();
        }

        public void visit(SectionCode n)
        {
            throw new NotImplementedException();
        }

        public void visit(ConstantSymbol n)
        {
            throw new NotImplementedException();
        }

        public void visit(VariableSymbol n)
        {
            throw new NotImplementedException();
        }

        public void visit(FunctionSymbol n)
        {
            throw new NotImplementedException();
        }

        public void visit(BuiltinFunctionSymbol n)
        {
            throw new NotImplementedException();
        }

        public void visit(MakeGlobal n)
        {
            throw new NotImplementedException();
        }

        public void visit(Xor n)
        {
            throw new NotImplementedException();
        }

        public void visit(Sub n)
        {
            throw new NotImplementedException();
        }

        public void visit(Shl n)
        {
            throw new NotImplementedException();
        }

        public void visit(Push n)
        {
            throw new NotImplementedException();
        }

        public void visit(Or n)
        {
            throw new NotImplementedException();
        }

        public void visit(Mult n)
        {
            throw new NotImplementedException();
        }

        public void visit(Leave n)
        {
            throw new NotImplementedException();
        }

        public void visit(Label n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpLE n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpGE n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpE n)
        {
            throw new NotImplementedException();
        }

        public void visit(Divide n)
        {
            throw new NotImplementedException();
        }

        public void visit(Comment n)
        {
            throw new NotImplementedException();
        }

        public void visit(And n)
        {
            throw new NotImplementedException();
        }

        public void visit(StringAsParameter n)
        {
            throw new NotImplementedException();
        }

        public void visit(TupleType t)
        {
            throw new NotImplementedException();
        }

        public void visit(UInt32Type t)
        {
            throw new NotImplementedException();
        }

        public void visit(UInt8Type t)
        {
            throw new NotImplementedException();
        }

        public void visit(Int64Type t)
        {
            throw new NotImplementedException();
        }

        public void visit(Int16Type t)
        {
            throw new NotImplementedException();
        }

        public void visit(HexaType t)
        {
            throw new NotImplementedException();
        }

        public void visit(BinaryType t)
        {
            throw new NotImplementedException();
        }

        public void visit(FloatType t)
        {
            throw new NotImplementedException();
        }

        public void visit(CharType t)
        {
            throw new NotImplementedException();
        }

        public string visit(RegisterOffset n)
        {
            throw new NotImplementedException();
        }

        public string visit(ParamRegister n)
        {
            throw new NotImplementedException();
        }

        public string visit(FPRegister n)
        {
            throw new NotImplementedException();
        }

        public string visit(OctalConstant n)
        {
            throw new NotImplementedException();
        }

        public string visit(ByteConstant n)
        {
            throw new NotImplementedException();
        }

        public abstract void visit(UInt32Literal n);
        public abstract void visit(UInt64Literal n);
        public abstract void visit(UInt16Literal n);
        public abstract void visit(StringLiteral n);
        public abstract void visit(UOctalLiteral n);
        public abstract void visit(Int64Literal n);
        public abstract void visit(Int16Literal n);
        public abstract void visit(HexaLiteral n);
        public abstract void visit(BinaryLiteral n);
        public abstract void visit(DoubleLiteral n);
        public abstract void visit(PlusExp n);
        public abstract void visit(IdentifierExp identifier);
        public abstract void visit(OverflowMultExp n);
        public abstract void visit(OrExp n);
        public abstract void visit(ModuloExp n);
        public abstract void visit(ExclamationExp n);
        public abstract void visit(BitwiseXorExp n);
        public abstract void visit(BitwiseOrExp n);
        public abstract void visit(BitwiseLeftShiftExp n);
        public abstract void visit(AndExp n);
        public abstract void visit(FunctionCallExp n);
        public abstract void visit(Base n);
        public abstract void visit(ParameterDeclaration n);
        public abstract void visit(TupleElement n);
        public abstract void visit(TupleElementList n);
    }
}