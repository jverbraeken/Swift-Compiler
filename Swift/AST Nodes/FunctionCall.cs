using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public class FunctionCall : ASTNode
    {
        public string AssemblyLocation { get; set; }
        public FunctionCall(LineContext context) : base(context)
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
