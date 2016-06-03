using Swift.AST_Nodes;
using Swift.Phrases;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class ExclamationExp : BinaryExp, Exp
    {
        public ExclamationExp(ILineContext context, Exp e1, Exp e2) : base(context, e1, e2)
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override ASTType accept(TypeVisitor v)
        {
            return v.visit(this);
        }
    }
}
