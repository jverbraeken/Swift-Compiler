using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class UInt32Type : ASTType
    {
        public UInt32Type(bool optional = false) : base(optional)
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}