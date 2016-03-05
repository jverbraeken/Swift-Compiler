using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int8Type : ASTType
    {
        public int Value { get; set; }
        public Int8Type(int value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}