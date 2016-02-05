using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Swift.Tokens;

namespace SwiftTests
{
    [TestClass]
    public class LexicalAnalyzerTest
    {
        [TestMethod]
        public void TestRegex()
        {
            Assert.AreEqual(true, Regex.Match("print(\"hoi\") //comments", LexicalAnalyzer.regexIdentity).Success);
            Assert.AreEqual(true, Regex.Match("(", LexicalAnalyzer.regexOpenRoundBracket).Success);
            Assert.AreEqual(false, Regex.Match("//hoi", LexicalAnalyzer.regexOperator).Success);
            Assert.AreEqual(true, Regex.Match("//hoi", LexicalAnalyzer.regexComment).Success);
            Assert.AreEqual(true, Regex.Match("3 + 2", LexicalAnalyzer.regexInt).Success);
        }

        [TestMethod]
        public void TestEatWhitespace()
        {
            Assert.AreEqual("asdf", LexicalAnalyzer.EatWhitespace("     asdf"));
            Assert.AreEqual("asdf/1234", LexicalAnalyzer.EatWhitespace("     asdf/1234"));
        }

        [TestMethod]
        public void TestGetTokens()
        {
            string[] input = new string[2] { "     print(\"hoi\") //comments", "let a = 3 + 2" };
            LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();
            Tuple<List<Token>, List<LineContext>> result = lexicalAnalyzer.GetTokens(input);
            List<Token> tokens = result.Item1;
            Assert.AreEqual(tokens[0].value, "print");
            Assert.AreEqual(tokens[2].value, "\"hoi\"");
            Assert.AreEqual(tokens[5].value, "a");
            Assert.AreEqual(tokens[7].value, "3");
            Assert.AreEqual(tokens[9].value, "2");
            Assert.AreEqual(tokens[0].type, Global.DataType.IDENTIFIER);
            Assert.AreEqual(tokens[1].type, Global.DataType.OPEN_ROUND_BRACKET);
            Assert.AreEqual(tokens[2].type, Global.DataType.STRING);
            Assert.AreEqual(tokens[3].type, Global.DataType.CLOSE_ROUND_BRACKET);
            Assert.AreEqual(tokens[4].type, Global.DataType.LET);
            Assert.AreEqual(tokens[5].type, Global.DataType.IDENTIFIER);
            Assert.AreEqual(tokens[6].type, Global.DataType.OPERATOR);
            Assert.AreEqual(tokens[7].type, Global.DataType.INT);
            Assert.AreEqual(tokens[8].type, Global.DataType.OPERATOR);
            Assert.AreEqual(tokens[9].type, Global.DataType.INT);
        }

        [TestMethod]
        public void TestIsLiteral()
        {
            Assert.AreEqual(true, LexicalAnalyzer.isLiteral("\"hoi dit is  een string\""));
            Assert.AreEqual(true, LexicalAnalyzer.isLiteral("5.1234"));
            Assert.AreEqual(true, LexicalAnalyzer.isLiteral("3"));
            Assert.AreEqual(true, LexicalAnalyzer.isLiteral("true"));
            Assert.AreEqual(true, LexicalAnalyzer.isLiteral("false"));
        }
    }
}
