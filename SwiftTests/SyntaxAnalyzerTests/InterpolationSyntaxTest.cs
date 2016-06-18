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
using System.Xml.Linq;

namespace SwiftTests.SyntaxAnalyzerTests
{
    delegate void TestFunctionCall(FunctionCallExp node);

    [TestClass]
    public class InterpolationSyntaxTest
    {
        SyntaxAnalyzer syntaxAnalyzer;
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
            Add(Global.DataType.STRING, "");
            Add(Global.DataType.STRINGINTERPOLATION);
            Add(Global.DataType.IDENTIFIER, "a");
            Add(Global.DataType.STRINGINTERPOLATIONEND);
            Add(Global.DataType.CLOSE_ROUND_BRACKET);
            Add(Global.DataType.ENDSTATEMENT);

            Base ast = syntaxAnalyzer.CheckSyntax(tokens, context, Global.InstructionSets.X86_64);

            Check(
@"<Base>
  <FunctionCallExp Name=""print"">
    <ParameterCall>
      <StringLiteral>
        <StringElement ElementType = ""quotedTextItem"" Value = """" />
        <StringElement ElementType = ""interpolation"">
          <IdentifierExp ID = ""a"" />
        </StringElement>
      </StringLiteral>
    </ParameterCall>
  </FunctionCallExp>
</Base> ", ast, false, false);
        }

        [TestMethod]
        public void TestVariableString()
        {
            Add(Global.DataType.IDENTIFIER, "print");
            Add(Global.DataType.OPEN_ROUND_BRACKET);
            Add(Global.DataType.STRING, "");
            Add(Global.DataType.STRINGINTERPOLATION);
            Add(Global.DataType.IDENTIFIER, "a");
            Add(Global.DataType.STRINGINTERPOLATIONEND);
            Add(Global.DataType.STRING, "foo");
            Add(Global.DataType.CLOSE_ROUND_BRACKET);
            Add(Global.DataType.ENDSTATEMENT);

            Base ast = syntaxAnalyzer.CheckSyntax(tokens, context, Global.InstructionSets.X86_64);

            Check(
@"<Base>
  <FunctionCallExp Name=""print"">
    <ParameterCall>
      <StringLiteral>
        <StringElement ElementType = ""quotedTextItem"" Value = """" />
        <StringElement ElementType = ""interpolation"">
          <IdentifierExp ID = ""a"" />
        </StringElement>
        <StringElement ElementType = ""quotedTextItem"" Value = ""foo"" />
      </StringLiteral>
    </ParameterCall>
  </FunctionCallExp>
</Base> ", ast, false, false);
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

        private void Check(string str, ASTNode ast, bool parseLineContext, bool parseLineScope)
        {
            Assert.AreEqual(str.Replace(" ", ""), ast.ToXML(new XMLParser.XMLProperties(parseLineContext, parseLineScope)).ToString().Replace(" ", ""));
        }
    }
}
