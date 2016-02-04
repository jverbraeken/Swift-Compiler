using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Swift.Tokens;

namespace SwiftTests
{
    [TestClass]
    public class SyntaxAnalyzerTest
    {

        [TestMethod]
        public void TestCheckSyntax()
        {
            Token tmpToken;
            LineContext tmpContext;
            List<Token> tokens = new List<Token>();
            tmpToken = new Token(Global.DataType.Identifier);
            tmpToken.value = "print";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.Open_round_bracket);
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.String);
            tmpToken.value = "\"hoi\"";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.Close_round_bracket);
            tokens.Add(tmpToken);

            List<LineContext> context = new List<LineContext>();
            tmpContext = new LineContext(1, 1);
            context.Add(tmpContext);
            tmpContext = new LineContext(1, 1);
            context.Add(tmpContext);
            tmpContext = new LineContext(1, 1);
            context.Add(tmpContext);
            tmpContext = new LineContext(1, 1);
            context.Add(tmpContext);

            SyntaxAnalyzer syntaxAnalyer = new SyntaxAnalyzer();
            AST result = syntaxAnalyer.CheckSyntax(tokens, context);
            Assert.AreEqual("Swift.ASTFunctionCall", result.getChildren()[0].GetType().ToString());
            Assert.AreEqual("print", (result.getChildren()[0] as ASTFunctionCall).GetName());
            Assert.AreEqual("\"hoi\"", (((result.getChildren()[0] as ASTFunctionCall).GetArgs()[0]) as ASTString).GetValue());
        }
    }
}
