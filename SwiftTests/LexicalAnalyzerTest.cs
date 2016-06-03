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
        LexicalAnalyzer lexicalAnalyzer;

        [TestInitialize]
        public void Initialize()
        {
            lexicalAnalyzer = new LexicalAnalyzer();
        }

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
            Assert.AreEqual((new string[] { "asdf", "5" }).ToString(), LexicalAnalyzer.EatWhitespace("     asdf").ToString());
            Assert.AreEqual((new string[] { "asdf/1234", "5" }).ToString(), LexicalAnalyzer.EatWhitespace("     asdf/1234").ToString());
        }

        [TestMethod]
        public void TestPrint()
        {
            string[] input = new string[] { "print(\"hoi\") //comments" };
            Tuple<List<Token>, List<ILineContext>> result = lexicalAnalyzer.GetTokens(input);
            List<Token> tokens = result.Item1;
            Assert.AreEqual(tokens[0].value, "print");
            Assert.AreEqual(tokens[2].value, "hoi");
            Assert.AreEqual(tokens[0].type, Global.DataType.IDENTIFIER);
            Assert.AreEqual(tokens[1].type, Global.DataType.OPEN_ROUND_BRACKET);
            Assert.AreEqual(tokens[2].type, Global.DataType.STRING);
            Assert.AreEqual(tokens[3].type, Global.DataType.CLOSE_ROUND_BRACKET);
            Assert.AreEqual(tokens[4].type, Global.DataType.ENDSTATEMENT);
        }

        [TestMethod]
        public void TestAssignment()
        {
            string[] input = new string[] { "let a = 3 + 2" };
            Tuple<List<Token>, List<ILineContext>> result = lexicalAnalyzer.GetTokens(input);
            List<Token> tokens = result.Item1;
            Assert.AreEqual(tokens[1].value, "a");
            Assert.AreEqual(tokens[3].value, "3");
            Assert.AreEqual(tokens[5].value, "2");
            Assert.AreEqual(tokens[0].type, Global.DataType.LET);
            Assert.AreEqual(tokens[1].type, Global.DataType.IDENTIFIER);
            Assert.AreEqual(tokens[2].type, Global.DataType.OPERATOR);
            Assert.AreEqual(tokens[3].type, Global.DataType.INT);
            Assert.AreEqual(tokens[4].type, Global.DataType.OPERATOR);
            Assert.AreEqual(tokens[5].type, Global.DataType.INT);
            Assert.AreEqual(tokens[6].type, Global.DataType.ENDSTATEMENT);
        }

        [TestMethod]
        public void TestMultiline()
        {
            string[] input = new string[] { "     print(\"hoi\") //comments", "let a = 3 + 2" };
            Tuple<List<Token>, List<ILineContext>> result = lexicalAnalyzer.GetTokens(input);
            List<Token> tokens = result.Item1;
            Assert.AreEqual(tokens[0].value, "print");
            Assert.AreEqual(tokens[2].value, "hoi");
            Assert.AreEqual(tokens[6].value, "a");
            Assert.AreEqual(tokens[8].value, "3");
            Assert.AreEqual(tokens[10].value, "2");
            Assert.AreEqual(tokens[0].type, Global.DataType.IDENTIFIER);
            Assert.AreEqual(tokens[1].type, Global.DataType.OPEN_ROUND_BRACKET);
            Assert.AreEqual(tokens[2].type, Global.DataType.STRING);
            Assert.AreEqual(tokens[3].type, Global.DataType.CLOSE_ROUND_BRACKET);
            Assert.AreEqual(tokens[4].type, Global.DataType.ENDSTATEMENT);
            Assert.AreEqual(tokens[5].type, Global.DataType.LET);
            Assert.AreEqual(tokens[6].type, Global.DataType.IDENTIFIER);
            Assert.AreEqual(tokens[7].type, Global.DataType.OPERATOR);
            Assert.AreEqual(tokens[8].type, Global.DataType.INT);
            Assert.AreEqual(tokens[9].type, Global.DataType.OPERATOR);
            Assert.AreEqual(tokens[10].type, Global.DataType.INT);
            Assert.AreEqual(tokens[11].type, Global.DataType.ENDSTATEMENT);
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
        
        [TestMethod]
        public void TestStringInterpolation()
        {
            string[] input = new string[] { "let multiplier = 3",
                "let message = \"\\(multiplier) times 2.5 is \\(Double(multiplier) * 2.5)\"",
                "print(message)" };
            Tuple<List<Token>, List<ILineContext>> result = lexicalAnalyzer.GetTokens(input);
            List<Token> tokens = result.Item1;
            AssertControl control = new AssertControl(tokens);
            control.assertNext(Global.DataType.LET);
            control.assertNext(Global.DataType.IDENTIFIER, "multiplier");
            control.assertNext(Global.DataType.OPERATOR, "=");
            control.assertNext(Global.DataType.INT, "3");
            control.assertNext(Global.DataType.ENDSTATEMENT);
            control.assertNext(Global.DataType.LET);
            control.assertNext(Global.DataType.IDENTIFIER, "message");
            control.assertNext(Global.DataType.OPERATOR, "=");
            control.assertNext(Global.DataType.STRINGINTERPOLATION);
            control.assertNext(Global.DataType.IDENTIFIER, "multiplier");
            control.assertNext(Global.DataType.STRINGINTERPOLATIONEND);
            control.assertNext(Global.DataType.STRING, " times 2.5 is ");
            control.assertNext(Global.DataType.STRINGINTERPOLATION);
            control.assertNext(Global.DataType.DOUBLETYPE);
            control.assertNext(Global.DataType.OPEN_ROUND_BRACKET);
            control.assertNext(Global.DataType.IDENTIFIER, "multiplier");
            control.assertNext(Global.DataType.CLOSE_ROUND_BRACKET);
            control.assertNext(Global.DataType.OPERATOR, "*");
            control.assertNext(Global.DataType.DOUBLE, "2.5");
            control.assertNext(Global.DataType.STRINGINTERPOLATIONEND);
            control.assertNext(Global.DataType.ENDSTATEMENT);
            control.assertNext(Global.DataType.IDENTIFIER, "print");
            control.assertNext(Global.DataType.OPEN_ROUND_BRACKET);
            control.assertNext(Global.DataType.IDENTIFIER, "message");
            control.assertNext(Global.DataType.CLOSE_ROUND_BRACKET);
            control.assertNext(Global.DataType.ENDSTATEMENT);
        }





        private class AssertControl {
            private List<Token> tokens;
            private int counter;

            public AssertControl(List<Token> tokens)
            {
                this.tokens = tokens;
                counter = 0;
            }

            public void assertNext(Global.DataType type, string value)
            {
                Assert.AreEqual(type, tokens[counter].type);
                Assert.AreEqual(value, tokens[counter].value);
                counter++;
            }

            public void assertNext(Global.DataType type)
            {
                Assert.AreEqual(type, tokens[counter].type);
                counter++;
            }

            public void assertNext(string value)
            {
                Assert.AreEqual(value, tokens[counter].value);
                counter++;
            }
        }
    }
}
