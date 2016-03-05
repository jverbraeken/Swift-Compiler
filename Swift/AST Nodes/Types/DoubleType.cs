using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class DoubleType : ASTType
    {
        public double Value { get; set; }
        public DoubleType(double value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}