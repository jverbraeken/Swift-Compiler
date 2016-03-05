using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class StringType : ASTType
    {
        public StringType()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}