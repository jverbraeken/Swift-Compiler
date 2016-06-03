using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Swift.Tokens;
using Swift.AST_Nodes;

namespace SwiftTests.SyntaxAnalyzerTests
{
    [TestClass]
    public class SyntaxAnalyzerTest
    {

        [TestMethod]
        [ExpectedException(typeof(SyntaxAnalyzer.NoTypeSpecifiedException))]
        public void TestEatDeclarationWithoutType()
        {
            Token tmpToken;
            ILineContext tmpContext;
            List<Token> tokens = new List<Token>();
            tmpToken = new Token(Global.DataType.VAR);
            tmpToken.value = "var";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.IDENTIFIER);
            tmpToken.value = "a";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.ENDSTATEMENT);
            tokens.Add(tmpToken);

            List<ILineContext> context = new List<ILineContext>();
            for (int i = 0; i < 3; i++)
            {
                tmpContext = new LineContext(1, 1);
                context.Add(tmpContext);
            }

            SyntaxAnalyzer syntaxAnalyer = new SyntaxAnalyzer();
            Base result = syntaxAnalyer.CheckSyntax(tokens, context, Global.InstructionSets.X86_64);
        }

        [TestMethod]
        public void TestEatDeclarationWithAssignment()
        {
            Token tmpToken;
            ILineContext tmpContext;
            List<Token> tokens = new List<Token>();
            tmpToken = new Token(Global.DataType.VAR);
            tmpToken.value = "var";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.IDENTIFIER);
            tmpToken.value = "a";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "=";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "3";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "+";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "2";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.ENDSTATEMENT);
            tokens.Add(tmpToken);

            List<ILineContext> context = new List<ILineContext>();
            for (int i = 0; i < 7; i++)
            {
                tmpContext = new LineContext(1, 1);
                context.Add(tmpContext);
            }

            SyntaxAnalyzer syntaxAnalyer = new SyntaxAnalyzer();
            TestVisitor testVisitor = new TestVisitor();
            Base result = syntaxAnalyer.CheckSyntax(tokens, context, Global.InstructionSets.X86_64);
            /*Assert.AreEqual(Global.ASTType.VAR_DECLARATION, result.GetChildren()[0].GetType());
            Assert.AreEqual("a", result.GetChildren()[0].GetName());
            Assert.AreEqual(Global.ASTType.ASSIGNMENT, result.GetChildren()[1].GetType());*/

        }

        [TestMethod]
        public void TestExpressionPlus()
        {
            Token tmpToken;
            ILineContext tmpContext;
            List<Token> tokens = new List<Token>();
            tmpToken = new Token(Global.DataType.VAR);
            tmpToken.value = "var";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.IDENTIFIER);
            tmpToken.value = "a";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "=";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "3";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "+";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "2";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.ENDSTATEMENT);
            tokens.Add(tmpToken);

            List<ILineContext> context = new List<ILineContext>();
            for (int i = 0; i < 7; i++)
            {
                tmpContext = new LineContext(1, 1);
                context.Add(tmpContext);
            }

            SyntaxAnalyzer syntaxAnalyer = new SyntaxAnalyzer();
            TestVisitor testVisitor = new TestVisitor();
            Base result = syntaxAnalyer.CheckSyntax(tokens, context, Global.InstructionSets.X86_64);
            Assert.AreEqual(new VarDeclaration(null).GetType(), result.Children[0].GetType());
            Assert.AreEqual("a", ((VarDeclaration)result.Children[0]).Name.Name);
            Assert.AreEqual(new Assignment(null).GetType(), result.Children[1].GetType());
            Assert.AreEqual("a", ((Assignment) result.Children[1]).LHS.Name);
            Assert.AreEqual(new PlusExp(null, null, null).GetType(), ((Assignment)result.Children[1]).RHS.GetType());
            Assert.AreEqual(new Int64Literal(null, null).GetType(), ((PlusExp)((Assignment)result.Children[1]).RHS).e1.GetType());
            Assert.AreEqual(new Int64Literal(null, null).GetType(), ((PlusExp)((Assignment)result.Children[1]).RHS).e2.GetType());
            Assert.AreEqual("3", ((Int64Literal) ((PlusExp) ((Assignment)result.Children[1]).RHS).e1).Value);
            Assert.AreEqual("2", ((Int64Literal)((PlusExp)((Assignment)result.Children[1]).RHS).e2).Value);
        }

        [TestMethod]
        public void TestExpressionPlusMult()
        {
            Token tmpToken;
            ILineContext tmpContext;
            List<Token> tokens = new List<Token>();
            tmpToken = new Token(Global.DataType.VAR);
            tmpToken.value = "var";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.IDENTIFIER);
            tmpToken.value = "a";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "=";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "1";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "+";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "2";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "*";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "3";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.ENDSTATEMENT);
            tokens.Add(tmpToken);

            List<ILineContext> context = new List<ILineContext>();
            for (int i = 0; i < 9; i++)
            {
                tmpContext = new LineContext(1, 1);
                context.Add(tmpContext);
            }

            SyntaxAnalyzer syntaxAnalyer = new SyntaxAnalyzer();
            TestVisitor testVisitor = new TestVisitor();
            Base result = syntaxAnalyer.CheckSyntax(tokens, context, Global.InstructionSets.X86_64);
            Assert.AreEqual(new VarDeclaration(null).GetType(), result.Children[0].GetType());
            Assert.AreEqual("a", ((VarDeclaration)result.Children[0]).Name.Name);
            Assert.AreEqual(new Assignment(null).GetType(), result.Children[1].GetType());
            Assert.AreEqual("a", ((Assignment)result.Children[1]).LHS.Name);
            Assert.AreEqual(new PlusExp(null, null, null).GetType(), ((Assignment)result.Children[1]).RHS.GetType());
            Assert.AreEqual(new Int64Literal(null, null).GetType(), ((PlusExp)((Assignment)result.Children[1]).RHS).e1.GetType());
            Assert.AreEqual(new MultiplicationExp(null, null, null).GetType(), ((PlusExp)((Assignment)result.Children[1]).RHS).e2.GetType());
            Assert.AreEqual("1", ((Int64Literal)((PlusExp)((Assignment)result.Children[1]).RHS).e1).Value);
            Assert.AreEqual(new Int64Literal(null, null).GetType(), ((MultiplicationExp) ((PlusExp)((Assignment)result.Children[1]).RHS).e2).e1.GetType());
            Assert.AreEqual(new Int64Literal(null, null).GetType(), ((MultiplicationExp) ((PlusExp)((Assignment)result.Children[1]).RHS).e2).e2.GetType());
            Assert.AreEqual("2", ((Int64Literal)((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e2).e1).Value);
            Assert.AreEqual("3", ((Int64Literal)((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e2).e2).Value);
        }

        [TestMethod]
        public void TestExpressionMultPlusMult()
        {
            Token tmpToken;
            ILineContext tmpContext;
            List<Token> tokens = new List<Token>();
            tmpToken = new Token(Global.DataType.VAR);
            tmpToken.value = "var";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.IDENTIFIER);
            tmpToken.value = "a";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "=";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "1";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "*";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "2";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "+";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "3";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.OPERATOR);
            tmpToken.value = "*";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.INT);
            tmpToken.value = "4";
            tokens.Add(tmpToken);
            tmpToken = new Token(Global.DataType.ENDSTATEMENT);
            tokens.Add(tmpToken);

            List<ILineContext> context = new List<ILineContext>();
            for (int i = 0; i < 11; i++)
            {
                tmpContext = new LineContext(1, 1);
                context.Add(tmpContext);
            }

            SyntaxAnalyzer syntaxAnalyer = new SyntaxAnalyzer();
            TestVisitor testVisitor = new TestVisitor();
            Base result = syntaxAnalyer.CheckSyntax(tokens, context, Global.InstructionSets.X86_64);
            Assert.AreEqual(new VarDeclaration(null).GetType(), result.Children[0].GetType());
            Assert.AreEqual("a", ((VarDeclaration)result.Children[0]).Name.Name);
            Assert.AreEqual(new Assignment(null).GetType(), result.Children[1].GetType());
            Assert.AreEqual("a", ((Assignment)result.Children[1]).LHS.Name);
            Assert.AreEqual(new PlusExp(null, null, null).GetType(), ((Assignment)result.Children[1]).RHS.GetType());

            Assert.AreEqual(new MultiplicationExp(null, null, null).GetType(), ((PlusExp)((Assignment)result.Children[1]).RHS).e2.GetType());
            Assert.AreEqual(new Int64Literal(null, null).GetType(), ((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e1).e1.GetType());
            Assert.AreEqual(new Int64Literal(null, null).GetType(), ((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e1).e2.GetType());
            Assert.AreEqual("1", ((Int64Literal)((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e1).e1).Value);
            Assert.AreEqual("2", ((Int64Literal)((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e1).e2).Value);

            Assert.AreEqual(new MultiplicationExp(null, null, null).GetType(), ((PlusExp)((Assignment)result.Children[1]).RHS).e2.GetType());
            Assert.AreEqual(new Int64Literal(null, null).GetType(), ((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e2).e1.GetType());
            Assert.AreEqual(new Int64Literal(null, null).GetType(), ((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e2).e2.GetType());
            Assert.AreEqual("3", ((Int64Literal)((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e2).e1).Value);
            Assert.AreEqual("4", ((Int64Literal)((MultiplicationExp)((PlusExp)((Assignment)result.Children[1]).RHS).e2).e2).Value);
        }
    }
}
