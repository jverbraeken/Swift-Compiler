using Swift.AST_Nodes;
using Swift.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Symbols
{
    public class VariableSymbol : Symbol
    {
        public ASTType Type { get; set; }
        public VariableSymbol(string name, ASTType type) : base(name)
        {
            Type = type;
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
