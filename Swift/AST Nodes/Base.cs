using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public class Base : ASTNode
    {
        public List<ASTNode> Children { get; set; }

        public Base(LineContext context) : base(context)
        {
            Children = new List<ASTNode>();
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
