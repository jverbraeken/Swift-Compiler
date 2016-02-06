using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class SemanticAnalyzer
    {
        private List<Table> tables;

        public List<Table> GenerateSymbolTables(ASTNode ast)
        {
            tables = new List<Table>();
            Table swiftTable = new Table(null);
            Symbol printSymbol = new Symbol("print", Global.DataType.BUILTIN_FUNC);
            List<Type> lst = new List<Type>();
            lst.Add(new StringType());
            printSymbol.SetParameters(lst);
            lst = new List<Type>();
            lst.Add(new VoidType());
            printSymbol.SetReturnTypes(lst);
            swiftTable.insert(printSymbol);
            tables.Add(swiftTable);
            tables.Add(new Table(tables[0]));

            foreach (ASTNode node in ast.GetChildren())
            {
                node.SetScope(tables[1]);
                switch (node.GetType().ToString())
                {
                    //case "Swift.A
                }
            }

            return tables;
        }

        public void CheckSemantic(ASTNode ast)
        {
            foreach (ASTNode node in ast.GetChildren())
            {
                switch (node.GetType())
                {
                    case Global.ASTType.FUNCTION_CALL: CheckFunction(node); break;
                }
            }
        }

        private void CheckFunction(ASTNode node)
        {
            string name = node.GetName();
            List<ASTNode> args = node.GetChildren();
            Table scope = node.GetScope();
            while (scope != tables[0])
            {
                Symbol reference = scope.lookup(name);
                if (reference != null) //The identifier exists in the current scope
                {
                    if (args.Count == reference.GetParameters().Count)
                    {

                    }
                    else
                    {
                        Swift.error("The number of parameters you supplied when calling \"" + name + "\" at the line " + node.GetContext().GetLine().ToString() + ", column " + node.GetContext().GetPos().ToString() + " does not match the required number of parameters as defined in the function", 1);
                    }
                }
            }
        }
    }
}
