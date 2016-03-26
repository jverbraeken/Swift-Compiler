using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int8Type : ASTType
    {
        public Int8Type()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}