using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Swift.Tokens;
using Swift.AST_Nodes;

namespace SwiftTests
{
    [TestClass]
    public class SemanticAnalyzerTest
    {

        [TestMethod]
        public void TestGenerateSymbolTables()
        {
            Base ast = new Base(new LineContext(1, 1));
            /*ASTNode astFunction = new ASTNode(Global.ASTType.FUNCTION_CALL, new ILineContext(1, 2));
            astFunction.SetName("print");
            ASTNode astString = new ASTNode(Global.ASTType.STRING, new ILineContext(2, 2));
            astString.SetName("testje");
            astFunction.SetChildren(new List<ASTNode> { astString });
            ast.SetChildren(new List<ASTNode> { astFunction });
            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            List<Table> tables = semanticAnalyzer.GenerateSymbolTables(ast);
            Assert.AreEqual("print", tables[0].lookup("print").GetName());*/
        }

        [TestMethod]
        public void TestCheckVarDeclaration()
        {
            Base ast = new Base(new LineContext(1, 1));
            /*ASTNode astVariable = new ASTNode(Global.ASTType.VAR_DECLARATION, new ILineContext(1, 2));
            astVariable.SetName("a");
            ast.SetChildren(new List<ASTNode> { astVariable });
            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            List<Table> tables = semanticAnalyzer.GenerateSymbolTables(ast);
            Assert.AreEqual("a", tables[1].lookup("a").GetName());*/
        }
    }
}
