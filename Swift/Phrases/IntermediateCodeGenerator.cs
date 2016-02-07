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
        public List<string> GenerateCode(string source, string dest, ASTNode ast, List<Table> tables)
        {
            result = new List<string>();
            w("file:" + source);
            w("section:constants");

            int teller = 0;
            foreach (ASTNode node in ast.GetChildren())
            {
                if (node.GetType() == Global.ASTType.STRING)
                {
                    w("define_constant_string:" + teller.ToString() + ":" + node.GetName());
                    node.SetAssemblyLocation(teller);
                    teller++;
                }
            }

            w("section:code");
            w("define_main_method");
            w("set_base_pointer");

            foreach (ASTNode node in ast.GetChildren())
            {
                switch (node.GetType())
                {
                    case Global.ASTType.FUNCTION_CALL: ExecuteFunctionCall(node);  break;
                }
            }

            w("get_base_pointer");
            w(".ident    \"Yontu: (Joost Verbraeken) BETA\"");
            return result;
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
