using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class HexaType : ASTType
    {
        public HexaType()
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}