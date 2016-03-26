using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class UInt8Type : ASTType
    {
        public UInt8Type()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}