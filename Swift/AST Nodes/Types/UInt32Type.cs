using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class UInt32Type : ASTType
    {
        public UInt32Type()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}