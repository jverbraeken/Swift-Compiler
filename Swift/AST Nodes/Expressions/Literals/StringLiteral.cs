using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes
{
    public class StringLiteral : ASTNode
    {
        public string Name { get; set; }
        public string AssemblyLocation { get; set; } // For large constant data types like strings
        public LineContext Context { get; set; }
        public StringLiteral(LineContext context) : base(context)
        {
            Context = context;
        }
    }
}
