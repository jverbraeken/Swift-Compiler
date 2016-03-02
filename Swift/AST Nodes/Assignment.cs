using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public class Assignment : ASTNode
    {
        public Identifier LHS { get; set; }
        public Exp RHS { get; set; }
        public Assignment(LineContext context) : base(context)
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
