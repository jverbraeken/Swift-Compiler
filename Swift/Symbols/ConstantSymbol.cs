using Swift.AST_Nodes;
using Swift.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Symbols
{
    public class ConstantSymbol : Symbol
    {
        public Exp Value { get; set; }
        public ASTType Type { get; set; }

        public ConstantSymbol(string name, ASTType type, Exp value) : base(name)
        {
            Type = type;
            Value = Value;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override ASTType accept(TypeVisitor v)
        {
            return v.visit(this);
        }
    }
}
