using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class DoubleType : ASTType
    {
        public DoubleType()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}