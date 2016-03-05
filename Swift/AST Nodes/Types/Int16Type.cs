using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class Int16Type : ASTType
    {
        public int Value { get; set; }
        public Int16Type(int value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}