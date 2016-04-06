using System;
using Swift.AST_Nodes;
using Swift.Phrases;
using Swift.Tokens;

namespace Swift
{
    public class UHexaLiteral : ASTNode, Exp
    {
        public string Value { get; set; }
        public UHexaLiteral(LineContext context, string value) : base(context)
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
