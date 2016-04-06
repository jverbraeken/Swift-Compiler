using System;
using Swift.AST_Nodes;
using Swift.Phrases;
using Swift.Tokens;

namespace Swift
{
    public class Int32Literal : ASTNode, Exp
    {
        public string Value { get; set; }
        public Int32Literal(LineContext context, string value) : base(context)
        {
            Value = value;
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
