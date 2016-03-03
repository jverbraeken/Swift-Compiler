using Swift.AST_Nodes;
using Swift.Tokens;

namespace Swift
{
    public class IntegerLiteral : ASTNode, Exp, AssTarget
    {
        public string Value { get; set; }
        public IntegerLiteral(LineContext context, string value) : base(context)
        {
            Value = value;
        }
        public override string accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
