using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int16Type : ASTType
    {
        public Int16Type(bool optional = false) : base(optional)
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}