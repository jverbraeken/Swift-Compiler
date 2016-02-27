using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Swift.Tokens;

namespace SwiftTests
{
    [TestClass]
    public class IntermediateCodeGeneratorTest
    {

        [TestMethod]
        public void TestGenerateCode()
        {
            ASTNode ast = new ASTNode(Global.ASTType.BASE, new LineContext(1, 1));
            ASTNode astFunction = new ASTNode(Global.ASTType.FUNCTION_CALL, new LineContext(1, 2));
            astFunction.SetName("print");
            ASTNode astString = new ASTNode(Global.ASTType.STRING, new LineContext(2, 2));
            astString.SetName("testje");
            astFunction.SetChildren(new List<ASTNode> { astString });
            ast.SetChildren(new List<ASTNode> { astFunction });

            List<Table> tables = new List<Table>();
            tables.Add(new Table(null));
            tables[0].insert(new Symbol("print", Global.DataType.BUILTIN_FUNC));
            astFunction.SetScope(tables[0]);
            astString.SetScope(tables[0]);

            IntermediateCodeGenerator intermediateCodeGenerator = new IntermediateCodeGenerator();
            List<string> result = intermediateCodeGenerator.GenerateCode("source", "dest", ast, tables);
            CollectionAssert.AreEqual(new List<String>(new string[] { "file:source", "section:constants", "define_constant_string:0:testje", "section:code", "define_main_method", "set_base_pointer", "call:print,constant,0", "get_base_pointer", ".ident    \"Yontu: (Joost Verbraeken) BETA\"" }), result);
        }
    }
}
