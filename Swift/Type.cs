using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    /// <summary>
    /// A type that can be used as parameter or return type for functions
    /// </summary>
    /// <param name="name">The name of the identifier as it will be recognized in the source code, e.g. i, foo, bar</param>
    /// <param name="type">The type of the identifier (Global.DataType)</param>
    /// <param name="attribute"></param>
    public class Type
    {
        private string name;
        private Global.DataType type;
        private bool isStatic;
        private object value; //for constants
        private int length; //for arrays
        private List<Symbol> parameters; //for functions
        private ASTNode returnType; //for functions
        private bool isReferenced; //if a symbol is never referenced it will be omitted in the compilation

        public Type(string name, Global.DataType type)
        {
            this.name = name;
            this.type = type;
        }

        public List<Symbol> GetParameters()
        {
            return parameters;
        }

        public ASTNode GetReturnType()
        {
            return returnType;
        }
    }
}
