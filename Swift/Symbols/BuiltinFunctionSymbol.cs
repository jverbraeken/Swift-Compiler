using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Symbols
{
    public class BuiltinFunctionSymbol : Symbol
    {
        public List<ASTType> Parameters { get; set; }
        public List<ASTType> ReturnTypes { get; set; }

        public BuiltinFunctionSymbol(string name) : base(name)
        {
        }
    }
}
