using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class CharType : ASTType
    {
        public char Value { get; set; }
        public CharType(char value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}