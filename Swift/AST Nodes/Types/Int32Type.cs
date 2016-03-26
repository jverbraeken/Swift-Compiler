using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int32Type : ASTType
    {
        public Int32Type()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}