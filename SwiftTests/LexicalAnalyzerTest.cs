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
