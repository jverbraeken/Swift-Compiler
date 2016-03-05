using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Symbols
{
    public class FunctionSymbol : Symbol
    {
        public List<ASTType> Parameters { get; set; }
        public List<ASTType> ReturnTypes { get; set; }

        public FunctionSymbol(string name) : base(name)
        {
        }
    }
}
