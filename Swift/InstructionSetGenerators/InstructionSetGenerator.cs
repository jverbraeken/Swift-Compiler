using Swift.Instructions;
using Swift.Instructions.Directives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.AssTargets;
using Swift.Symbols;

namespace Swift.InstructionSetGenerators
{
    public abstract class InstructionSetGenerator : Visitor
    {
        public System.IO.StreamWriter Stream { get; set;}

        public InstructionSetGenerator(System.IO.StreamWriter stream)
        {
            Stream = stream;
        }

        /// <summary>
        /// Write to File
        /// Writes a line of text to the file with indentation level 1.
        /// </summary>
        /// <param name="l">A single line of text to be written to the file</param>
        public void w(string l)
        {
            Stream.WriteLine("    " + l);
        }

        public abstract void processModules(string original_file, List<Module> modules);

        /// <summary>
        /// Write to File No Identation
        /// Writes a line of text to the file with indentation level 0.
        /// </summary>
        /// <param name="l">A single line of text to be written to the file</param>
        public void wn(string l)
        {
            Stream.WriteLine(l);
        }

        public abstract string visit(IntegerConstant n);
        public abstract string visit(ByteConstant n);
        public abstract string visit(BinaryConstant n);
        public abstract string visit(OctalConstant n);
        public abstract string visit(HexadecimalConstant n);
        public abstract string visit(Register n);
        public abstract string visit(RegisterOffset n);
        public abstract string visit(ParamRegister n);
        public abstract string visit(FPRegister n);
        // Instructions
        public abstract void visit(Add n);
        public abstract void visit(Call n);
        public abstract void visit(Comment n);
        public abstract void visit(Divide n);
        public abstract void visit(Label n);
        public abstract void visit(Lea n);
        public abstract void visit(Leave n);
        public abstract void visit(Move n);
        public abstract void visit(Mult n);
        public abstract void visit(StringAsParameter n);

        public void visit(Assignment n)
        {
            throw new NotImplementedException();
        }

        public void visit(ParameterDeclaration n)
        {
            throw new NotImplementedException();
        }

        public void visit(TupleElement n)
        {
            throw new NotImplementedException();
        }

        public void visit(TupleElementList n)
        {
            throw new NotImplementedException();
        }

        public void visit(ConstDeclaration n)
        {
            throw new NotImplementedException();
        }

        public void visit(VarDeclaration n)
        {
            throw new NotImplementedException();
        }

        public void visit(BitwiseAndExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(BitwiseLeftShiftExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(BitwiseNotExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(BitwiseOrExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(BitwiseRightShiftExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(BitwiseXorExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(ExclamationExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(ModuloExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(OrExp powerExp)
        {
            throw new NotImplementedException();
        }

        public void visit(Identifier identifier)
        {
            throw new NotImplementedException();
        }

        public void visit(OverflowAddExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(OverflowMultExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(OverflowSubExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(StringLiteral n)
        {
            throw new NotImplementedException();
        }

        public virtual void visit(TypeCast id)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(IdentifierType id)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(BinaryType id)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(OctalType id)
        {
            // Do nothing; leave the implementation to the main class
        }

        public virtual void visit(HexaType id)
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

        public virtual void visit(Int8Type t)
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

        public virtual void visit(TupleType t)
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

        public virtual void visit(BoolType t)
        {
            // Do nothing; leave the implementation to the main class
        }

        public void visit(BinaryLiteral n)
        {
            throw new NotImplementedException();
        }

        public void visit(BoolLiteral n)
        {
            throw new NotImplementedException();
        }

        public void visit(DoubleLiteral n)
        {
            throw new NotImplementedException();
        }

        public void visit(FloatLiteral n)
        {
            throw new NotImplementedException();
        }

        public void visit(HexaLiteral n)
        {
            throw new NotImplementedException();
        }

        public void visit(Int16Literal n)
        {
            throw new NotImplementedException();
        }

        public void visit(Int32Literal n)
        {
            throw new NotImplementedException();
        }

        public void visit(Int64Literal n)
        {
            throw new NotImplementedException();
        }

        public void visit(Int8Literal n)
        {
            throw new NotImplementedException();
        }

        public void visit(OctalLiteral n)
        {
            throw new NotImplementedException();
        }

        public void visit(UBinaryLiteral n)
        {
            throw new NotImplementedException();
        }

        public void visit(UHexaLiteral n)
        {
            throw new NotImplementedException();
        }

        public void visit(UInt16Literal n)
        {
            throw new NotImplementedException();
        }

        public void visit(UInt32Literal n)
        {
            throw new NotImplementedException();
        }

        public void visit(UInt64Literal n)
        {
            throw new NotImplementedException();
        }

        public void visit(UInt8Literal n)
        {
            throw new NotImplementedException();
        }

        public void visit(UOctalLiteral n)
        {
            throw new NotImplementedException();
        }

        public void visit(Literal n)
        {
            throw new NotImplementedException();
        }

        public void visit(PlusExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(IdentifierExp identifier)
        {
            throw new NotImplementedException();
        }

        public void visit(MultiplicationExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(MinusExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(DivisionExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(AndExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(FunctionCallExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(Base n)
        {
            throw new NotImplementedException();
        }

        public abstract void visit(Nope n);
        public abstract void visit(Pop n);
        public abstract void visit(Push n);
        public abstract void visit(Ret n);
        public abstract void visit(Sub n);
        // Instructions - Directives
        public abstract void visit(Debug n);
        public abstract void visit(MakeGlobal n);
        public abstract void visit(SectionCode n);

        public void visit(BuiltinFunctionSymbol n)
        {
            throw new NotImplementedException();
        }

        public void visit(ConstantSymbol n)
        {
            throw new NotImplementedException();
        }

        public void visit(FunctionSymbol n)
        {
            throw new NotImplementedException();
        }

        public void visit(VariableSymbol n)
        {
            throw new NotImplementedException();
        }

        public void visit(And n)
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

        public void visit(JumpE n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpG n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpGE n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpL n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpLE n)
        {
            throw new NotImplementedException();
        }

        public void visit(JumpNE n)
        {
            throw new NotImplementedException();
        }

        public void visit(Shl n)
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

        public void visit(Xor n)
        {
            throw new NotImplementedException();
        }

        public void visit(Or n)
        {
            throw new NotImplementedException();
        }
    }
}
