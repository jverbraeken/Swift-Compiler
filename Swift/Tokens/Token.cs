using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    /// <summary>
    /// An analysis of a lexeme. Its the first step of lexeme processing.
    /// </summary>
    public class Token
    {
        public Global.PrimitiveType primType;
        public Global.DataType type;
        public string value; // Often the name, the string, the number, ... (all stored as a string)
        public Token(Global.PrimitiveType primType, string value)
        {
            this.primType = primType;
            this.value = value;
        }
    }
}
