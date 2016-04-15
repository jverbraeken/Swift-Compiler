using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class UInt64Type : ASTType
    {
        public UInt64Type(bool optional = false) : base(optional)
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}