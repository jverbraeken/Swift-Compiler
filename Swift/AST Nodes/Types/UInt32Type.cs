using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class UInt32Type : ASTType
    {
        public uint Value { get; set; }
        public UInt32Type(uint value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}