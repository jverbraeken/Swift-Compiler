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
    public abstract class BinaryExp : ASTNode, Exp
    {
        public Exp e1 { get; set; }
        public Exp e2 { get; set; }

        public BinaryExp(LineContext context, Exp e1, Exp e2) : base(context)
        {
            this.e1 = e1;
            this.e2 = e2;
        }

        public abstract override void accept(Visitor v);

        public abstract ASTType accept(TypeVisitor v);
    }
}
