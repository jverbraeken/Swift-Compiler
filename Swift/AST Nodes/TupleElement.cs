using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public class TupleElement
    {
        public ASTType Type { get; set; }
        public string Name { get; set; }

        public TupleElement(ASTType type)
        {
            Type = type;
            Name = null;
        }

        public TupleElement(ASTType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
