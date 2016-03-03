using Swift.AST_Nodes;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class PowerExp : ASTNode, Exp
    {
        public Exp e1, e2;
        public PowerExp(LineContext context, Exp e1, Exp e2) : base(context)
        {
            this.e1 = e1;
            this.e2 = e2;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
