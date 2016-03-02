using Swift.Tokens;

namespace Swift.AST_Nodes.Types
{
    public class IntegerType : Type
    {
        public int f0;
        public IntegerType(int f0)
        {
            this.f0 = f0;
        }
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}