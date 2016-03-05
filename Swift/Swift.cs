using Swift.AST_Nodes;
using Swift.Instructions;
using Swift.InstructionSetGenerators;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    class Swift
    {
        private static List<Instruction> interCode;

        static void Main(string[] args)
        {
            string lookingFor = "";
            string source = "";
            string output = "";
            Global.InstructionSets architecture = Global.InstructionSets.X86_64;

            for (int i = 0; i < args.Length; i++)
            {
                if (lookingFor == "")
                {
                    if (args[i][0] == '-')
                    {
                        if (args[i] == "-o")
                            lookingFor = "output";
                        else if (args[i] == "-IS")
                            lookingFor = "instructionSet";
                        else
                            error("An invalid argument was supplied: " + args[i], -1);
                    }
                    else
                    {
                        if (source == "")
                            source = args[i];
                        else
                            error("The source can be supplied only once: " + args[i], -1);
                    }
                }
                else
                {
                    if (lookingFor == "output")
                        output = args[i];
                    else if (lookingFor == "instructionSet")
                    {
                        switch (args[i].ToLower())
                        {
                            case "x86": architecture = Global.InstructionSets.X86; break;
                            case "x86_64": architecture = Global.InstructionSets.X86_64; break;
                            default: error("An unknown Instruction Set was supplied: " + args[i], -1); break;
                        }
                    }
                }
            }
            Console.WriteLine("Swift Compiler by Joost Verbraeken");
            string[] text = System.IO.File.ReadAllLines(source);

            Tuple<List<Token>, List<LineContext>> lexicalOutput = (new LexicalAnalyzer()).GetTokens(text);
            List<Token> tokens = lexicalOutput.Item1;
            List<LineContext> context = lexicalOutput.Item2;

            Base ast = (new SyntaxAnalyzer()).CheckSyntax(tokens, context);

            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            List<Table> symbolTables = semanticAnalyzer.GenerateSymbolTables(ast);
            semanticAnalyzer.CheckSemantic(ast);

            interCode = (new IntermediateCodeGenerator()).GenerateCode(source, output, ast, symbolTables);

            interCode = CodeOptimizer.OptimizeCode(interCode);
            
            string result = CodeGenerator.MakeAssembly(source, output, interCode, architecture);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();
        }

        public static void print(string line)
        {
            Console.WriteLine(line);
        }
        public static void error(string line, int exitcode)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(line);
            Console.ReadLine();
            Environment.Exit(exitcode);
        }
    }
}
