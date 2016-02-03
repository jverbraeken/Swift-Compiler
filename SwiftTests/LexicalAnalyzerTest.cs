using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            List<Token> result = LexicalAnalyzer.GetTokens(input);
            Assert.AreEqual(result[0].value, "print");
            Assert.AreEqual(result[2].value, "\"hoi\"");
            Assert.AreEqual(result[5].value, "a");
            Assert.AreEqual(result[7].value, "3");
            Assert.AreEqual(result[9].value, "2");
            Assert.AreEqual(result[0].type, Global.DataType.Identifier);
            Assert.AreEqual(result[1].type, Global.DataType.Open_round_bracket);
            Assert.AreEqual(result[2].type, Global.DataType.String);
            Assert.AreEqual(result[3].type, Global.DataType.Close_round_bracket);
            Assert.AreEqual(result[4].type, Global.DataType.Let);
            Assert.AreEqual(result[5].type, Global.DataType.Identifier);
            Assert.AreEqual(result[6].type, Global.DataType.Operator);
            Assert.AreEqual(result[7].type, Global.DataType.Int);
            Assert.AreEqual(result[8].type, Global.DataType.Operator);
            Assert.AreEqual(result[9].type, Global.DataType.Int);
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
