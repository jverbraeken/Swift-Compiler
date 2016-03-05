using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class BooleanType : ASTType
    {
        public bool Value { get; set; }
        public BooleanType(bool value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}