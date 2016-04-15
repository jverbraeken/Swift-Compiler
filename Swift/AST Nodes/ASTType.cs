using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public abstract class ASTType
    {
        public bool Optional { get; set; }

        public ASTType(bool optional = false)
        {
            Optional = optional;
        }
        public abstract void accept(Visitor n);
    }
}
