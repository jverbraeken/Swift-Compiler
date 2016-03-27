using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Swift;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Swift.Tokens;
using Swift.AST_Nodes;
using Swift.Symbols;
using Swift.AST_Nodes.Types;
using Swift.Instructions.Directives;
using Swift.Instructions;
using Swift.AssTargets;
using Swift.Phrases;

namespace SwiftTests
{
    [TestClass]
    public class ProgramTest
    {
        AssTarget rbp = new Register(Global.Registers.STACKBASEPOINTER);
        AssTarget rsp = new Register(Global.Registers.STACKPOINTER);
        AssTarget rax = new Register(Global.Registers.ACCUMULATOR);
        AssTarget rbx = new Register(Global.Registers.BASE);
        AssTarget rcx = new Register(Global.Registers.COUNTER);
        AssTarget rdx = new Register(Global.Registers.DATA);
        AssTarget rdi = new Register(Global.Registers.DEST_INDEX);
        AssTarget rsi = new Register(Global.Registers.SRC_INDEX);
        AssTarget rip = new Register(Global.Registers.INSTRUCTIONPOINTER);

        [TestMethod]
        public void TestHelloWorld()
        {
            string[] text = new string[1];
            text[0] = "print(\"Hello World!\")";

            Tuple<List<Token>, List<LineContext>> lexicalOutput = (new LexicalAnalyzer()).GetTokens(text);
            List<Token> tokens = lexicalOutput.Item1;
            List<LineContext> context = lexicalOutput.Item2;

            Base ast = (new SyntaxAnalyzer()).CheckSyntax(tokens, context);

            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            List<Table> symbolTables = semanticAnalyzer.CheckSemantics(ast);

            List<Module> modules = (new IntermediateCodeGenerator()).GenerateCode("source", "output", ast, symbolTables);


            List<Module> test = new List<Module>();
            test.Add(new Module(new List<Instruction>(), new Dictionary<string, string>()));
            test[0].InterCode.Add(new SectionCode());
            test[0].InterCode.Add(new MakeGlobal("main"));
            test[0].InterCode.Add(new Label("main"));
            test[0].InterCode.Add(new Push(rbp));
            test[0].InterCode.Add(new Move(rsp, rbp));
            test[0].InterCode.Add(new Sub(new ByteConstant(4), rsp));
            test[0].InterCode.Add(new Sub(new ByteConstant(3), rsp));
            test[0].InterCode.Add(new Call("__main"));
            test[0].InterCode.Add(new Move(new IntegerConstant(5), new RegisterOffset(Global.Registers.STACKBASEPOINTER, -1)));
            test[0].InterCode.Add(new Move(new IntegerConstant(10), new RegisterOffset(Global.Registers.STACKBASEPOINTER, -2)));
            test[0].InterCode.Add(new Move(new RegisterOffset(Global.Registers.STACKBASEPOINTER, -2), rdx));
            test[0].InterCode.Add(new Move(new RegisterOffset(Global.Registers.STACKBASEPOINTER, -1), rax));
            test[0].InterCode.Add(new Add(rdx, rax));
            test[0].InterCode.Add(new Move(rax, new RegisterOffset(Global.Registers.STACKBASEPOINTER, -3)));
            test[0].InterCode.Add(new Move(new RegisterOffset(Global.Registers.STACKBASEPOINTER, -3), new ParamRegister(1)));
            test[0].InterCode.Add(new Lea(new RegisterOffset(Global.Registers.INSTRUCTIONPOINTER, ".LC0"), new ParamRegister(0)));
            test[0].InterCode.Add(new Call("printf"));
            test[0].InterCode.Add(new Nope());
            test[0].InterCode.Add(new Move(rbp, rsp));
            test[0].InterCode.Add(new Pop(rbp));
            test[0].InterCode.Add(new Ret());

            test[0].StringTable.Add("%d", ".LC0");

            Assert.IsTrue(ModuleListComparer.Compare(modules, test));
        }

        [TestMethod]
        public void TestAddTwoVariablesAndPrint()
        {
            string[] text = new string[4];
            text[0] = "let a = 5";
            text[1] = "let b = 10";
            text[2] = "let c = a + b";
            text[3] = "print(c)";

            Tuple<List<Token>, List<LineContext>> lexicalOutput = (new LexicalAnalyzer()).GetTokens(text);
            List<Token> tokens = lexicalOutput.Item1;
            List<LineContext> context = lexicalOutput.Item2;

            Base ast = (new SyntaxAnalyzer()).CheckSyntax(tokens, context);

            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            List<Table> symbolTables = semanticAnalyzer.CheckSemantics(ast);

            List<Module> modules = (new IntermediateCodeGenerator()).GenerateCode("source", "output", ast, symbolTables);


            List<Module> test = new List<Module>();
            test.Add(new Module(new List<Instruction>(), new Dictionary<string, string>()));
            test[0].InterCode.Add(new SectionCode());
            test[0].InterCode.Add(new MakeGlobal("main"));
            test[0].InterCode.Add(new Label("main"));
            test[0].InterCode.Add(new Push(rbp));
            test[0].InterCode.Add(new Move(rsp, rbp));
            test[0].InterCode.Add(new Sub(new ByteConstant(4), rsp));
            test[0].InterCode.Add(new Sub(new ByteConstant(3), rsp));
            test[0].InterCode.Add(new Call("__main"));
            test[0].InterCode.Add(new Move(new IntegerConstant(5), new RegisterOffset(Global.Registers.STACKBASEPOINTER, -1)));
            test[0].InterCode.Add(new Move(new IntegerConstant(10), new RegisterOffset(Global.Registers.STACKBASEPOINTER, -2)));
            test[0].InterCode.Add(new Move(new RegisterOffset(Global.Registers.STACKBASEPOINTER, -2), rdx));
            test[0].InterCode.Add(new Move(new RegisterOffset(Global.Registers.STACKBASEPOINTER, -1), rax));
            test[0].InterCode.Add(new Add(rdx, rax));
            test[0].InterCode.Add(new Move(rax, new RegisterOffset(Global.Registers.STACKBASEPOINTER, -3)));
            test[0].InterCode.Add(new Move(new RegisterOffset(Global.Registers.STACKBASEPOINTER, -3), new ParamRegister(1)));
            test[0].InterCode.Add(new Lea(new RegisterOffset(Global.Registers.INSTRUCTIONPOINTER, ".LC0"), new ParamRegister(0)));
            test[0].InterCode.Add(new Call("printf"));
            test[0].InterCode.Add(new Nope());
            test[0].InterCode.Add(new Move(rbp, rsp));
            test[0].InterCode.Add(new Pop(rbp));
            test[0].InterCode.Add(new Ret());

            test[0].StringTable.Add("%d", ".LC0");

            Assert.IsTrue(ModuleListComparer.Compare(modules, test));
        }
    }
}
