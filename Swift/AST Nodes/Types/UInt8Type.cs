using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class UInt8Type : ASTType
    {
        public uint Value { get; set; }
        public UInt8Type(uint value)
        {
            Value = value;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}