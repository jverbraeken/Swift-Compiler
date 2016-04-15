using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int64Type : ASTType
    {
        public Int64Type(bool optional = false) : base(optional)
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}