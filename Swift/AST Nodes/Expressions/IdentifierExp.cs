using Swift.AST_Nodes;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class IdentifierExp : ASTNode, Exp
    {
        public Identifier ID
        {
            get; set;
        }
        public IdentifierExp(LineContext context, Identifier id) : base(context)
        {
            ID = id;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
