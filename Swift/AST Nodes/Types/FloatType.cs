using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class FloatType : ASTType
    {
        public float Value { get; set; }
        public FloatType(float value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}