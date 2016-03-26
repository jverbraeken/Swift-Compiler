using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class UInt64Type : ASTType
    {
        public UInt64Type()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}