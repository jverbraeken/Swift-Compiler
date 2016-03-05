using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Phrases
{
    class SymbolTableGeneratorVisitor : VisitorAdapter
    {
        public override void visit(Assignment n)
        {
            string name = n.LHS.Name;
            Table scope = n.Scope;
            while (scope != null)
            {
                Symbol reference = scope.lookup(name);
                if (reference != null)
                {
                    reference.SetReferenced();
                    break;
                }
                scope = scope.GetReference();
            }
        }

        public override void visit(VarDeclaration n)
        {
            Table scope = n.Scope;
            Symbol sym = new Symbol(n.Name.Name, Global.DataType.VAR);
            scope.Add(sym);
        }
    }
}
