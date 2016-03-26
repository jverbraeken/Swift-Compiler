using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int16Type : ASTType
    {
        public Int16Type()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}