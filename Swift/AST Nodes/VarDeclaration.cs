using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public class VarDeclaration : ASTNode
    {
        public Identifier Name { get; set; }
        /// <summary>
        /// Type must be used when the type of the variable is declared directly, otherwise TypeByAssignment must be used
        /// </summary>
        public ASTType Type { get; set; }
        /// <summary>
        /// TypeByAssignment must be used when the type of the variable is declared indirectly, otherwise Type must be used
        /// </summary>
        public Assignment TypeByAssignment { get; set; } 

        public VarDeclaration(LineContext context) : base(context)
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
