using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public class ParameterCall
    {
        public Exp Value { get; set; }
        /// <summary>
        /// The external name of the ParameterDeclaration
        /// </summary>
        public string Name { get; set; }

        public ParameterCall(Exp value)
        {
            Value = value;
        }

        public ParameterCall(Exp value, string name)
        {
            Value = value;
            Name = name;
        }
    }
}
