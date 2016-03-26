using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swift.Phrases;

namespace Swift.AST_Nodes
{
    public class FunctionCallExp : ASTNode, Exp
    {
        public Identifier Name { get; set; }
        public List<ParameterCall> Args { get; set; }
        public FunctionCallExp(LineContext context) : base(context)
        {
            Args = new List<ParameterCall>();
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public ASTType accept(TypeVisitor v)
        {
            return v.visit(this);
        }
    }
}
