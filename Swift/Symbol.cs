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
        private List<Type> parameters; //for functions
        private List<Type> returnTypes; //for functions
        private bool isReferenced; //if a symbol is never referenced it will be omitted in the compilation

        public Symbol(string name, Global.DataType type)
        {
            this.name = name;
            this.type = type;
        }

        public void SetParameters(List<Type> parameters)
        {
            this.parameters = parameters;
        }

        public List<Type> GetParameters()
        {
            return parameters;
        }

        public void SetReturnTypes(List<Type> returnTypes)
        {
            this.returnTypes = returnTypes;
        }

        public List<Type> GetReturnTypes()
        {
            return returnTypes;
        }
    }
}
