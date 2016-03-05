using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes.Types
{
    public class VoidType : ASTType
    {
        public VoidType()
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
