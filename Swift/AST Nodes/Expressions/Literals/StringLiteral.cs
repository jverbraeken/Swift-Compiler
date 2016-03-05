using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public class StringLiteral : ASTNode, Exp
    {
        public string Name { get; set; }
        public string AssemblyLocation { get; set; } // For large constant data types like strings
        public StringLiteral(LineContext context, string name) : base(context)
        {
            Name = name;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
