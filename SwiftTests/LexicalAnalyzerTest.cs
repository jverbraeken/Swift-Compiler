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

        public void TestStringToLexemes()
        {
            List<string> result = LexicalAnalyzer.StringToLexemes("     print(\"hoi\") //comments");
            System.Diagnostics.Debug.WriteLine(result.ToString());
            Assert.AreEqual(result[0], "print");
            Assert.AreEqual(result[1], "(");
            Assert.AreEqual(result[2], "\"hoi\"");
            Assert.AreEqual(result[3], ")");
        }
    }
}
