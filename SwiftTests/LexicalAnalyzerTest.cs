using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;

namespace SwiftTests
{
    [TestClass]
    public class LexicalAnalyzerTest
    {
        [TestMethod]
        public void TestEatWhitespace()
        {
            Assert.AreEqual("asdf", LexicalAnalyzer.EatWhitespace("     asdf"));
            Assert.AreEqual("asdf/1234", LexicalAnalyzer.EatWhitespace("     asdf/1234"));
        }

        [TestMethod]
        public void TestGetLexemes()
        {
            string[] input = new string[2] { "     print(\"hoi\") //comments", "let a = 3 + 2" };
            List<Token> result = LexicalAnalyzer.GetLexemes(input);
            Assert.AreEqual(result[0].value, "print");
            Assert.AreEqual(result[1].value, "(");
            Assert.AreEqual(result[2].value, "\"hoi\"");
            Assert.AreEqual(result[3].value, ")");
            Assert.AreEqual(result[4].value, "let");
            Assert.AreEqual(result[5].value, "a");
            Assert.AreEqual(result[6].value, "=");
            Assert.AreEqual(result[7].value, "3");
            Assert.AreEqual(result[8].value, "+");
            Assert.AreEqual(result[9].value, "2");
            Assert.AreEqual(result[0].primType, Global.PrimitiveType.IDENTIFIER);
            Assert.AreEqual(result[1].primType, Global.PrimitiveType.PUNCTUATION);
            Assert.AreEqual(result[2].primType, Global.PrimitiveType.LITERAL);
            Assert.AreEqual(result[3].primType, Global.PrimitiveType.PUNCTUATION);
            Assert.AreEqual(result[4].primType, Global.PrimitiveType.KEYWORD);
            Assert.AreEqual(result[5].primType, Global.PrimitiveType.IDENTIFIER);
            Assert.AreEqual(result[6].primType, Global.PrimitiveType.OPERATOR);
            Assert.AreEqual(result[7].primType, Global.PrimitiveType.LITERAL);
            Assert.AreEqual(result[8].primType, Global.PrimitiveType.OPERATOR);
            Assert.AreEqual(result[9].primType, Global.PrimitiveType.LITERAL);
        }

        [TestMethod]
        public void TestStringToLexemes()
        {
            List<string> result = LexicalAnalyzer.StringToLexemes("     print(\"hoi\") //comments");
            Assert.AreEqual(result[0], "print");
            Assert.AreEqual(result[1], "(");
            Assert.AreEqual(result[2], "\"hoi\"");
            Assert.AreEqual(result[3], ")");

            result = LexicalAnalyzer.StringToLexemes("/* testje */");
            Assert.AreEqual(result.Count, 0);

            result = LexicalAnalyzer.StringToLexemes("  /* testje */      5.1234  hallo");
            Assert.AreEqual(result[0], "5.1234");
            Assert.AreEqual(result[1], "hallo");
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
