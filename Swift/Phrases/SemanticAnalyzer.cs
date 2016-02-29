using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class SemanticAnalyzer : Visitor
    {
        private List<Table> tables;

        public List<Table> GenerateSymbolTables(ASTNode ast)
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

            foreach (ASTNode node in ast.GetChildren())
            {
                node.SetScope(tables[1]);
                switch (node.GetType())
                {
                    case Global.ASTType.VAR_DECLARATION: AddVariable(node); break;
                    case Global.ASTType.ASSIGNMENT: AssignVariable(node); break;
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

        private void AddVariable(ASTNode node)
        {
            Table scope = node.GetScope();
            Symbol sym = new Symbol(node.GetName(), Global.DataType.VAR);
            sym.SetStackLocation(scope.StackSize++);
            scope.insert(sym);
        }

        private void AssignVariable(ASTNode node)
        {
            string name = node.GetName();
            Table scope = node.GetScope();
            while (scope != null)
            {
                Symbol reference = scope.lookup(name);
                if (reference != null && reference.IsReferenced())
                {
                    //Evaluate the expression
                }
            }
        }

        private void CheckFunction(ASTNode node)
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
        }



        Exp Visitor.visit(AndExp n)
        {
            throw new NotImplementedException();
        }

        Exp Visitor.visit(BitwiseComplementExp n)
        {
            throw new NotImplementedException();
        }

        Exp Visitor.visit(DivisionExp n)
        {
            throw new NotImplementedException();
        }

        Exp Visitor.visit(ExclamationExp n)
        {
            throw new NotImplementedException();
        }

        Exp Visitor.visit(MinusExp n)
        {
            throw new NotImplementedException();
        }

        Exp Visitor.visit(ModuloExp n)
        {
            throw new NotImplementedException();
        }

        Exp Visitor.visit(MultiplicationExp n)
        {
            throw new NotImplementedException();
        }

        Exp Visitor.visit(OrExp powerExp)
        {
            throw new NotImplementedException();
        }

        Exp Visitor.visit(Identifier identifier)
        {
            return identifier;
        }

        Exp Visitor.visit(PlusExp n)
        {
            Exp e1 = n.e1.accept(this);
            Exp e2 = n.e2.accept(this);
            if (e1.GetType() != e2.GetType())
            {
                //Werkt dit???
                Swift.error("Incompatible types at line " + n.GetContext().GetLine(), 1);
            }
        }

        Exp Visitor.visit(PowerExp n)
        {
            throw new NotImplementedException();
        }

        Exp Visitor.visit(IntegerLiteral n)
        {
            return n;
        }
    }
}
