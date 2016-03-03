using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class SemanticAnalyzer : VisitorAdapter
    {
        private List<Table> tables;

        public List<Table> GenerateSymbolTables(Base ast)
        {
            tables = new List<Table>();
            Table swiftTable = new Table(null);
            Symbol printSymbol = new Symbol("print", Global.DataType.BUILTIN_FUNC);
            List<Global.ASTType> lst = new List<Global.ASTType>();
            lst.Add(Global.ASTType.STRING);
            printSymbol.SetParameters(lst);
            lst = new List<Global.ASTType>();
            printSymbol.SetReturnTypes(lst);
            swiftTable.insert(printSymbol);
            tables.Add(swiftTable);
            tables.Add(new Table(tables[0]));

            foreach (ASTNode node in ast.Children)
            {
                node.Scope = tables[1];
                node.accept(this);
            }

            return tables;
        }

        public void CheckSemantic(Base ast)
        {
            foreach (ASTNode node in ast.Children)
            {
                /*
                ***********
                I'M GOING TO NEED MORE THAN ONE VISITOR PROBABLY
                **************
            
            switch (node.GetType())
                {
                    case Global.ASTType.FUNCTION_CALL: CheckFunction(node); break;
                }*/
            }
        }

        /*private void CheckFunction(ASTNode node)
        {
            string name = node.GetName();
            List<ASTNode> args = node.GetChildren();
            Table scope = node.GetScope();
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
        }*/













        public override void visit(VarDeclaration n)
        {
            Table scope = n.Scope;
            Symbol sym = new Symbol(n.Name.Name, Global.DataType.VAR);
            scope.Add(sym);
        }

        public override void visit(Assignment n)
        {
            string name = n.LHS.Name;
            Table scope = n.Scope;
            while (scope != null)
            {
                Symbol reference = scope.lookup(name);
                if (reference != null && reference.IsReferenced())
                {
                    //Evaluate the expression
                }
            }
        }
    }
}
