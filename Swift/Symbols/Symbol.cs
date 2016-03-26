using Swift.AST_Nodes;
using Swift.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Symbols
{
    /// <summary>
    /// Stores an identifier of a certain scope. Thus some tokens will be combined into a symbol.
    /// </summary>
    /// <param name="name">The name of the identifier as it will be recognized in the source code, e.g. i, foo, bar</param>
    /// <param name="type">The type of the identifier (Global.DataType)</param>
    /// <param name="attribute"></param>
    public abstract class Symbol
    {
        public string Name { get; set; }
        private bool isStatic;
        private bool isReferenced; // if a symbol is never referenced it will be omitted in the compilation
        private int stackLocation;
        /// <summary>
        /// Gets or sets stackLocation starting at 1 and increasing by 1, so when generating the code the stack location should be multiplied by, for example, 4 on 32 bit systems.
        /// </summary>
        public int StackLocation
        {
            get
            {
                return stackLocation;
            }
            set
            {
                if (value <= 0)
                    throw new ProtectedDataException("You're trying to overwrite data that's not allocated to variables in the current scope.");
                stackLocation = value;
            }
        }

        public Symbol(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Sets isReferenced to true
        /// </summary>
        public void SetReferenced()
        {
            isReferenced = true;
        }

        /// <summary>
        /// If a symbol is never referenced it will be omitted in the compilation
        /// </summary>
        /// <returns></returns>
        public bool IsReferenced()
        {
            return isReferenced;
        }

        [Serializable()]
        private class ProtectedDataException : System.Exception
        {
            public ProtectedDataException() : base() { }
            public ProtectedDataException(string message) : base(message) { }
            public ProtectedDataException(string message, System.Exception inner) : base(message, inner) { }
            protected ProtectedDataException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        }

        public abstract void accept(Visitor v);
        public abstract ASTType accept(TypeVisitor v);
    }
}
