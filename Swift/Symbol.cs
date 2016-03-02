using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    /// <summary>
    /// Stores an identifier of a certain scope. Thus some tokens will be combined into a symbol.
    /// </summary>
    /// <param name="name">The name of the identifier as it will be recognized in the source code, e.g. i, foo, bar</param>
    /// <param name="type">The type of the identifier (Global.DataType)</param>
    /// <param name="attribute"></param>
    public class Symbol
    {
        private string name;
        private Global.DataType type;
        private bool isStatic;
        private object value; //for constants
        private int length; //for arrays
        private List<Global.ASTType> parameters; //for functions
        private List<Global.ASTType> returnTypes; //for functions
        private bool isReferenced; //if a symbol is never referenced it will be omitted in the compilation
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

        public Symbol(string name, Global.DataType type)
        {
            this.name = name;
            this.type = type;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public void SetParameters(List<Global.ASTType> parameters)
        {
            this.parameters = parameters;
        }

        public List<Global.ASTType> GetParameters()
        {
            return parameters;
        }

        public void SetReturnTypes(List<Global.ASTType> returnTypes)
        {
            this.returnTypes = returnTypes;
        }

        public List<Global.ASTType> GetReturnTypes()
        {
            return returnTypes;
        }

        public new Global.DataType GetType()
        {
            return type;
        }

        /// <summary>
        /// Sets isReferenced to true
        /// </summary>
        public void SetReferenced()
        {
            isReferenced = true;
        }

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
    }
}
