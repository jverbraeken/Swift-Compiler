using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class OctalType : ASTType
    {
        public OctalType(bool optional = false) : base(optional)
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}