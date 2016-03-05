using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int64Type : ASTType
    {
        public int Value { get; set; }
        public Int64Type(int value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}