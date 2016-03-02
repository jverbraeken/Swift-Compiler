using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public abstract class ASTNode
    {
        public LineContext Context { get; set; } //For debugging, gives the programmer an idea where the node is located in the original source code
        public ASTNode(LineContext context)
        {
            Context = context;
        }

        public abstract void accept(Visitor v);
    }
}
