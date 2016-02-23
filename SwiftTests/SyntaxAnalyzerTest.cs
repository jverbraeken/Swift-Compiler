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
            tmpToken = new Token(Global.DataType.IDENTIFIER);
            tmpToken.value = "print";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPEN_ROUND_BRACKET);
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.STRING);
            tmpToken.value = "\"hoi\"";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.CLOSE_ROUND_BRACKET);
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
            ASTNode result = syntaxAnalyer.CheckSyntax(tokens, context);
            Assert.AreEqual(Global.ASTType.BASE, result.GetType());
            Assert.AreEqual(Global.ASTType.FUNCTION_CALL, result.GetChildren()[0].GetType());
            Assert.AreEqual("print", result.GetChildren()[0].GetName());
            Assert.AreEqual("\"hoi\"", result.GetChildren()[0].GetChildren()[0].GetName());
        }

        [TestMethod]
        public void TestEatDeclaration()
        {
            Token tmpToken;
            LineContext tmpContext;
            List<Token> tokens = new List<Token>();
            tmpToken = new Token(Global.DataType.VAR);
            tmpToken.value = "var";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.IDENTIFIER);
            tmpToken.value = "a";
            tokens.Add(tmpToken);

            List<LineContext> context = new List<LineContext>();
            tmpContext = new LineContext(1, 1);
            context.Add(tmpContext);
            tmpContext = new LineContext(1, 1);
            context.Add(tmpContext);

            SyntaxAnalyzer syntaxAnalyer = new SyntaxAnalyzer();
            ASTNode result = syntaxAnalyer.CheckSyntax(tokens, context);
            Assert.AreEqual(Global.ASTType.BASE, result.GetType());
            Assert.AreEqual(Global.ASTType.VAR_DECLARATION, result.GetChildren()[0].GetType());
            Assert.AreEqual("a", result.GetChildren()[0].GetName());
        }
    }
}
