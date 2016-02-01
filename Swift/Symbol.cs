using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    /// <summary>
    /// Stores an identifier of a certain scope. Thus some tokens will be combined into a symbol (an identifier).
    /// </summary>
    class Symbol
    {
        public string name;
        public Global.DataType type;
        public bool isStatic;
        public Global.DataType attribute;
        public object value; //for constants
        public int length; //for arrays
        public List<Symbol> parameters; //for functions

        public static Symbol symbol(string name, Global.DataType type, Global.DataType attribute)
        {
            Symbol output = new Symbol();
            output.name = name;
            output.type = type;
            output.attribute = attribute;
            return output;
        }
    }
}
