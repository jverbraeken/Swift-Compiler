using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class UInt16Type : ASTType
    {
        public UInt16Type()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}