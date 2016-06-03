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
    public interface Visitor
    {
        // AssTargets
        string visit(IntegerConstant n);
        string visit(ByteConstant n);
        string visit(BinaryConstant n);
        string visit(OctalConstant n);
        string visit(HexadecimalConstant n);
        string visit(ParamRegister n);
        string visit(FPRegister n);
        string visit(Register n);
        string visit(RegisterOffset n);
        // AST Nodes
        void visit(Assignment n);
        void visit(Base n);
        void visit(ConstDeclaration n);
        void visit(FunctionCallExp n);
        void visit(ParameterDeclaration n);
        void visit(TupleElement n);
        void visit(TupleElementList n);
        void visit(VarDeclaration n);
        // AST Nodes - Expressions
        void visit(AndExp n);
        void visit(BitwiseAndExp n);
        void visit(BitwiseLeftShiftExp n);
        void visit(BitwiseNotExp n);
        void visit(BitwiseOrExp n);
        void visit(BitwiseRightShiftExp n);
        void visit(BitwiseXorExp n);
        void visit(DivisionExp n);
        void visit(ExclamationExp n);
        void visit(MinusExp n);
        void visit(ModuloExp n);
        void visit(MultiplicationExp n);
        void visit(OrExp n);
        void visit(OverflowAddExp n);
        void visit(OverflowMultExp n);
        void visit(OverflowSubExp n);
        void visit(IdentifierExp identifier);
        void visit(Identifier identifier);
        void visit(PlusExp n);
        // AST Nodes - Expressions - Literals
        void visit(Literal n);
        void visit(BoolLiteral n);
        void visit(DoubleLiteral n);
        void visit(FloatLiteral n);
        void visit(BinaryLiteral n);
        void visit(OctalLiteral n);
        void visit(HexaLiteral n);
        void visit(Int8Literal n);
        void visit(Int16Literal n);
        void visit(Int32Literal n);
        void visit(Int64Literal n);
        void visit(UBinaryLiteral n);
        void visit(UOctalLiteral n);
        void visit(UHexaLiteral n);
        void visit(StringLiteral n);
        void visit(UInt8Literal n);
        void visit(UInt16Literal n);
        void visit(UInt32Literal n);
        void visit(UInt64Literal n);
        // AST Nodes - Types
        void visit(TypeCast n);
        void visit(BoolType t);
        void visit(CharType t);
        void visit(DoubleType t);
        void visit(FloatType t);
        void visit(IdentifierType id);
        void visit(BinaryType t);
        void visit(OctalType t);
        void visit(HexaType t);
        void visit(Int8Type t);
        void visit(Int16Type t);
        void visit(Int32Type t);
        void visit(Int64Type t);
        void visit(StringType t);
        void visit(UInt8Type t);
        void visit(UInt16Type t);
        void visit(UInt32Type t);
        void visit(UInt64Type t);
        void visit(TupleType t);
        // Instructions
        void visit(Add n);
        void visit(And n);
        void visit(Call n);
        void visit(Comment n);
        void visit(Compare n);
        void visit(Divide n);
        void visit(Jump n);
        void visit(JumpE n);
        void visit(JumpG n);
        void visit(JumpGE n);
        void visit(JumpL n);
        void visit(JumpLE n);
        void visit(JumpNE n);
        void visit(Label n);
        void visit(Lea n);
        void visit(Leave n);
        void visit(Move n);
        void visit(Mult n);
        void visit(Nope n);
        void visit(Or n);
        void visit(Pop n);
        void visit(Push n);
        void visit(Ret n);
        void visit(Shl n);
        void visit(Shr n);
        void visit(StringAsParameter n);
        void visit(Sub n);
        void visit(Xchg n);
        void visit(Xor n);
        // Instructions - Directives
        void visit(Debug n);
        void visit(MakeGlobal n);
        void visit(SectionCode n);
        //Symbols
        void visit(BuiltinFunctionSymbol n);
        void visit(ConstantSymbol n);
        void visit(FunctionSymbol n);
        void visit(VariableSymbol n);
    }
}