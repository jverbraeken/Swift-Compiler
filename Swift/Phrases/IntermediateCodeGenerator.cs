using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class IntermediateCodeGenerator
    {
        List<string> result;
        int teller;

        public List<string> GenerateCode(string source, string dest, ASTNode ast, List<Table> tables)
        {
            result = new List<string>();
            w("file:\"" + source + "\"");
            w("section:constants");

            teller = 0;
            SearchConstants(ast);

            w("section:var_unitialised");

            teller = 0;
            SearchUnitialised(ast);

            w("section:var_initialised");

            teller = 0;
            SearchUnitialised(ast);

            w("section:code");
            w("define_main_method");
            w("set_base_pointer");
            w("reserve_stack");
            w("call:%SETUP_C%");

            foreach (ASTNode node in ast.GetChildren())
            {
                switch (node.GetType())
                {
                    case Global.ASTType.FUNCTION_CALL: ExecuteFunctionCall(node);  break;
                    case Global.ASTType.VAR_DECLARATION: DeclareVariable(node); break;
                    case Global.ASTType.ASSIGNMENT: AssignVariable(node); break;
                }
            }

            w("get_base_pointer");
            w("return");
            w("comment:\"Yontu: (Joost Verbraeken) BETA\"");
            return result;
        }

        private void SearchConstants(ASTNode ast)
        {
            foreach (ASTNode node in ast.GetChildren())
            {
                switch (node.GetType())
                {
                    case Global.ASTType.STRING:
                        w("define_constant_string:" + teller.ToString() + ":" + node.GetName());
                        node.SetAssemblyLocation(teller);
                        teller++; break;
                }
                SearchConstants(node);
            }
        }

        private void SearchInitialised(ASTNode ast)
        {
            bool waitForAssignment = false;
            foreach (ASTNode node in ast.GetChildren())
            {
                if (waitForAssignment)
                {
                    if (node.GetType() == Global.ASTType.ASSIGNMENT)
                    {
                        Symbol symbol = node.GetScope().lookup(node.GetExpression1().accept(this));
                        switch (symbol.GetType())
                    }
                    waitForAssignment = false;
                }
                switch (node.GetType())
                {
                    case Global.ASTType.VAR_DECLARATION:
                        if (node.GetScope().GetReference() == null)
                        {
                            waitForAssignment = true;
                        }
                            break;
                }
                if (!waitForAssignment)
                    SearchInitialised(node);
            }
        }

        private void DeclareVariable(ASTNode node)
        {
            string name = node.GetName();
            List<ASTNode> args = node.GetChildren();
            Table scope = node.GetScope();
            if (scope.GetReference() != null)
            {

            }
        }

        private void ExecuteFunctionCall(ASTNode node)
        {
            string name = node.GetName();
            List<ASTNode> args = node.GetChildren();
            Table scope = node.GetScope();
            while (scope != null)
            {
                Symbol reference = scope.lookup(name);
                if (reference != null) //The identifier exists in the current scope
                {
                    if (reference.GetType() == Global.DataType.BUILTIN_FUNC) //The function is builtin
                    {
                        switch (reference.GetName())
                        {
                            case "print": ExecutePrint(node); break;
                        }
                    }
                }
                scope = scope.GetReference();
            }
        }

        private void ExecutePrint(ASTNode node)
        {
            if (node.GetChildren()[0].GetType() == Global.ASTType.STRING)
                w("call:print,constant," + node.GetChildren()[0].GetAssemblyLocation());
        }



        private void w(string str)
        {
            result.Add(str);
        }
    }
}
