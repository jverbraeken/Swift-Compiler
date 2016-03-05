using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int32Type : ASTType
    {
        public int Value { get; set; }
        public Int32Type(int value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}