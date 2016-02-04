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
        public List<Table> GenerateSymbolTables(AST ast)
        {
            tables = new List<Table>();
            Table swiftTable = new Table(null);
            Symbol printSymbol = new Symbol("print", Global.DataType.Builtin_Func);
            swiftTable.insert(printSymbol);
            return new List<Table>();
        }

        public void CheckSemantic(AST ast)
        {

        }
    }
}
