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

namespace Swift.InstructionSetGenerators
{
    abstract class InstructionSetGenerator : Visitor
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

        // Instructions
        public abstract void visit(Add n);
        public abstract void visit(Call n);
        public abstract void visit(Comment n);
        public abstract void visit(Label n);
        public abstract void visit(Leave n);
        public abstract void visit(Move n);

        public void visit(Assignment n)
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

        public void visit(BitwiseComplementExp n)
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

        public void visit(PowerExp n)
        {
            throw new NotImplementedException();
        }

        public void visit(StringLiteral n)
        {
            throw new NotImplementedException();
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

        public void visit(IntegerLiteral n)
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

        public void visit(FunctionCall n)
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
        public abstract void visit(File n);
        public abstract void visit(MakeGlobal n);
        public abstract void visit(SectionCode n);
    }
}
