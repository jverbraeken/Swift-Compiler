using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class IntegerLiteral : ASTNode, Exp
    {
        public string f0;
        public IntegerLiteral(LineContext context, string f0) : base(context)
        {
            this.f0 = f0;
        }
        public Exp accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
