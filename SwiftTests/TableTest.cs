using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Swift.Tokens;
using Swift.AST_Nodes;
using Swift.Symbols;
using Swift.AST_Nodes.Types;

namespace SwiftTests
{
    [TestClass]
    public class TableTest
    {

        [TestMethod]
        public void TestLookup()
        {
            Table swiftTable = new Table(null, 0, null);
            BuiltinFunctionSymbol printSymbol = new BuiltinFunctionSymbol("print");
            List<ParameterDeclaration> lst = new List<ParameterDeclaration>();
            ParameterDeclaration par = new ParameterDeclaration(null, new Int64Type(), null);
            lst.Add(par);
            printSymbol.Parameters = lst;
            printSymbol.ReturnValue = new TupleType();
            swiftTable.Insert(printSymbol);
            Assert.IsNotNull(swiftTable.Lookup("print", new List<ASTType>() { new Int64Type() }));
        }
    }
}
