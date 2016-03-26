using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class CharType : ASTType
    {
        public CharType()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}