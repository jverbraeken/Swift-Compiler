using Swift.AST_Nodes;
using Swift.Tokens;

namespace Swift
{
    public class IntegerLiteral : ASTNode, Exp
    {
        public string Value { get; set; }
        public IntegerLiteral(LineContext context, string value) : base(context)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
