using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Swift.Tokens;
using Swift.AST_Nodes;
using Swift.Instructions;
using Swift.AssTargets;
using NSubstitute;

namespace SwiftTests
{
    [TestClass]
    public class IntermediateCodeGeneratorTest
    {
        IntermediateCodeGenerator generator;

        [TestInitialize]
        public void init()
        {
            generator = new IntermediateCodeGenerator();
        }

        [TestMethod]
        public void TestAsciiToInt()
        {
            Assert.AreEqual(97, IntermediateCodeGenerator.AsciiToInt("a"));
            Assert.AreEqual(25185, IntermediateCodeGenerator.AsciiToInt("ab"));
            Assert.AreEqual(6513249, IntermediateCodeGenerator.AsciiToInt("abc"));
        }

        [TestMethod]
        public void TestPrintOneString()
        {
            ILineContext context = Substitute.For<ILineContext>();
            Base ast = new Base(null);
            ast.Children.Add(new FunctionCallExp(null));
            ((FunctionCallExp)ast.Children[0]).Name = new Identifier("print");
            //((FunctionCallExp)ast.Children[0]).Args.Add(new ParameterCall(context, new StringLiteral(null, "Hello World!")));
            Table builtinTable = SemanticAnalyzer.CreateBuiltinSymbols();
            List<Module> modules = generator.GenerateCode("", "", ast, new List<Table>() { builtinTable, new Table(builtinTable, Global.Scope.BuiltinScope, "builtin") });
            List<Instruction> instructions = modules[0].InterCode;
            Assert.IsTrue(instructions[7] is StringAsParameter);
            Assert.AreEqual(".LC0", ((StringAsParameter)instructions[7]).Name);
            Assert.AreEqual(0, ((StringAsParameter)instructions[7]).Number);
            Assert.IsTrue(instructions[8] is Call);
            Assert.AreEqual("printf", ((Call)instructions[8]).Name);
        }

        [TestMethod]
        public void TestPrintOneInteger()
        {
            ILineContext context = Substitute.For<ILineContext>();
            Base ast = new Base(null);
            ast.Children.Add(new FunctionCallExp(null));
            ((FunctionCallExp)ast.Children[0]).Name = new Identifier("print");
            ((FunctionCallExp)ast.Children[0]).Args.Add(new ParameterCall(context, new Int64Literal(null, "5")));
            Table builtinTable = SemanticAnalyzer.CreateBuiltinSymbols();
            List<Module> modules = generator.GenerateCode("", "", ast, new List<Table>() { builtinTable, new Table(builtinTable, Global.Scope.BuiltinScope, "builtin") });
            List<Instruction> instructions = modules[0].InterCode;
            Assert.IsTrue(instructions[7] is Move);
            Assert.IsTrue(((Move)instructions[7]).To is ParamRegister);
            Assert.AreEqual(1, ((ParamRegister) ((Move)instructions[7]).To).Position);

            Assert.IsTrue(instructions[8] is Lea);
            Assert.IsTrue(((Lea)instructions[8]).From is RegisterOffset);
            Assert.AreEqual(".LC0", ((RegisterOffset) ((Lea)instructions[8]).From).LabelOffset);
            Assert.AreEqual(Global.Registers.INSTRUCTIONPOINTER, ((RegisterOffset)((Lea)instructions[8]).From).Register);
            Assert.IsTrue(((Lea)instructions[8]).To is ParamRegister);
            Assert.AreEqual(0, ((ParamRegister)((Lea)instructions[8]).To).Position);

            Assert.IsTrue(instructions[9] is Call);
            Assert.AreEqual("printf", ((Call)instructions[9]).Name);
        }

        [TestMethod]
        public void TestPrintFiveIntegers()
        {
            ILineContext context = Substitute.For<ILineContext>();
            Base ast = new Base(null);
            ast.Children.Add(new FunctionCallExp(null));
            ((FunctionCallExp)ast.Children[0]).Name = new Identifier("print");
            ((FunctionCallExp)ast.Children[0]).Args.Add(new ParameterCall(context, new Int64Literal(null, "5")));
            Table builtinTable = SemanticAnalyzer.CreateBuiltinSymbols();
            List<Module> modules = generator.GenerateCode("", "", ast, new List<Table>() { builtinTable, new Table(builtinTable, Global.Scope.BuiltinScope, "builtin") });
            List<Instruction> instructions = modules[0].InterCode;
            Assert.IsTrue(instructions[7] is Move);
            Assert.IsTrue(((Move)instructions[7]).To is ParamRegister);
            Assert.AreEqual(1, ((ParamRegister)((Move)instructions[7]).To).Position);

            Assert.IsTrue(instructions[8] is Lea);
            Assert.IsTrue(((Lea)instructions[8]).From is RegisterOffset);
            Assert.AreEqual(".LC0", ((RegisterOffset)((Lea)instructions[8]).From).LabelOffset);
            Assert.AreEqual(Global.Registers.INSTRUCTIONPOINTER, ((RegisterOffset)((Lea)instructions[8]).From).Register);
            Assert.IsTrue(((Lea)instructions[8]).To is ParamRegister);
            Assert.AreEqual(0, ((ParamRegister)((Lea)instructions[8]).To).Position);

            Assert.IsTrue(instructions[9] is Call);
            Assert.AreEqual("printf", ((Call)instructions[9]).Name); //ff expres fout gedaan om de test te laten mislukken
        }
    }
}
