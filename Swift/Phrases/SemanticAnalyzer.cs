using Swift.AST_Nodes;
using Swift.AST_Nodes.Types;
using Swift.Phrases;
using Swift.Symbols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class SemanticAnalyzer
    {
        List<Table> tables;
        SymbolTableGeneratorVisitor symbolTableGeneratorVisitor;

        public List<Table> GenerateSymbolTables(Base ast)
        {
            symbolTableGeneratorVisitor = new SymbolTableGeneratorVisitor();
            tables = new List<Table>();
            Table swiftTable = new Table(null);
            BuiltinFunctionSymbol printSymbol = new BuiltinFunctionSymbol("print");
            List<ASTType> lst = new List<ASTType>();
            lst.Add(new StringType());
            printSymbol.Parameters = lst;
            lst = new List<ASTType>();
            printSymbol.ReturnTypes = lst;
            swiftTable.insert(printSymbol);
            tables.Add(swiftTable);
            tables.Add(new Table(tables[0]));

            foreach (ASTNode node in ast.Children)
            {
                node.Scope = tables[1];
                node.accept(symbolTableGeneratorVisitor);
            }

            return tables;
        }

        public void CheckSemantic(Base ast)
        {
            foreach (ASTNode node in ast.Children)
            {
                node.Scope = tables[1];
                node.accept(symbolTableGeneratorVisitor);
            }
        }

        private void CheckFunction(FunctionCall node)
        {
            string name = node.Name.Name;
            List<Exp> args = node.Args;
            Table scope = node.Scope;
            Symbol reference = scope.lookup(name);
            if (reference != null) //The identifier exists in the current scope
            {
                if (args.Count == reference.GetParameters().Count)
                {
                    for (int i = 0; i < args.Count; i++)
                    {
                        if (!(args[i].GetType() == reference.GetParameters()[i]))
                            Swift.error("The type of the parameter you supplied when calling \"" + name + "\" at the line " + args[i].GetContext().GetLine().ToString() + ", column " + args[i].GetContext().GetPos().ToString() + " is not the same type as required by the function", 1);
                    }
                }
                else
                {
                    Swift.error("The number of parameters you supplied when calling \"" + name + "\" at the line " + node.GetContext().GetLine().ToString() + ", column " + node.GetContext().GetPos().ToString() + " does not match the required number of parameters as defined in the function", 1);
                }
            }
            else if (scope == null)
                Swift.error("The function you called could not be found, line " + node.GetContext().GetLine().ToString() + ", column " + node.GetContext().GetPos().ToString(), 1);
        }














    }
}
