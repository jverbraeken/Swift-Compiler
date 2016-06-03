using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using Swift.AST_Nodes;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

namespace SwiftTests.SyntaxAnalyzerTests
{
    delegate void TestFunctionCall(FunctionCallExp node);

    [TestClass]
    public class InterpolationSyntaxTest
    {
        SyntaxAnalyzer syntaxAnalyzer;
        Token token;
        List<Token> tokens = new List<Token>();
        ILineContext _context = Substitute.For<ILineContext>();
        List<ILineContext> context = new List<ILineContext>();

        [TestInitialize]
        public void init()
        {
            syntaxAnalyzer = new SyntaxAnalyzer();
        }

        [TestCleanup]
        public void cleanup()
        {
            tokens.Clear();
        }

        [TestMethod]
        public void TestVariable()
        {
            Add(Global.DataType.IDENTIFIER, "print");
            Add(Global.DataType.OPEN_ROUND_BRACKET);
            Add(Global.DataType.STRINGINTERPOLATION);
            Add(Global.DataType.IDENTIFIER, "a");
            Add(Global.DataType.STRINGINTERPOLATIONEND);
            Add(Global.DataType.CLOSE_ROUND_BRACKET);
            Add(Global.DataType.ENDSTATEMENT);

            Base ast = syntaxAnalyzer.CheckSyntax(tokens, context, Global.InstructionSets.X86_64);

             TestFunctionCall testFunctionCall = delegate(FunctionCallExp node) {
                Assert.AreEqual(node.Name, "print");
            };

            Assert.IsTrue(ast.Children[0] is FunctionCallExp);
            testFunctionCall((FunctionCallExp)ast.Children[0]);
    }

        private void Add(Global.DataType type)
        {
            tokens.Add(new Token(type));
            context.Add(_context);
        }

        private void Add(Global.DataType type, string str)
        {
            tokens.Add(new Token(type, str));
            context.Add(_context);
        }

        private void Check(ASTNode node, Type type)
        {
            
        }
    }
}
