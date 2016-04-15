using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int8Type : ASTType
    {
        public Int8Type(bool optional = false) : base(optional)
        {
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}