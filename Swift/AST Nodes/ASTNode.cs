using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public abstract class ASTNode
    {
        ASTNode[] children;
        LineContext context;
        public ASTNode(LineContext context)
        {
            this.context = context;
        }
    }
}
