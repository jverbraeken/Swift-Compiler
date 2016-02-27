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
    }
}
