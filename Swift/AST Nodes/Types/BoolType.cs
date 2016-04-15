using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class BoolType : ASTType
    {
        public BoolType(bool optional = false) : base(optional)
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}