using Swift.AST_Nodes;
using Swift.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    /// <summary>
    /// *******************                 IS THIS STILL USED?????             **************************
    /// </summary>
    public class SymbolToTableVisitor : VisitorAdapter
    {
        Dictionary<Tuple<string, List<ASTType>>, Symbol> dictionary;
        public SymbolToTableVisitor(Dictionary<Tuple<string, List<ASTType>>, Symbol> dictionary)
        {
            this.dictionary = dictionary;
        }

        /*public override void visit(FunctionSymbol n)
        {
            dictionary.Add(Tuple.Create<string, List<ASTType>>(n.Name, n.Parameters), n);
        }*/
    }
}
