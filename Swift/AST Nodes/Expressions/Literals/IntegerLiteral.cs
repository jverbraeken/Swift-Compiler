using Swift.Tokens;

namespace Swift
{
    public class IntegerLiteral : ASTNode, Exp
    {
        public int f0;
        public IntegerLiteral(LineContext context, string f0) : base(context)
        {
            this.f0 = int.Parse(f0);
        }
        public Type accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
