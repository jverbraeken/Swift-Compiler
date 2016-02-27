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
        private static List<string> interCode;

        static void Main(string[] args)
        {
            string lookingFor = "";
            string source = "";
            string output = "";

            for (int i = 0; i < args.Length; i++)
            {
                if (lookingFor == "")
                {
                    if (args[i][0] == '-')
                    {
                        if (args[i] == "-o")
                            lookingFor = "output";
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
                }
            }
            Console.WriteLine("Swift Compiler by Joost Verbraeken");
            string[] text = System.IO.File.ReadAllLines(source);

            Tuple<List<Token>, List<LineContext>> lexicalOutput = (new LexicalAnalyzer()).GetTokens(text);
            List<Token> tokens = lexicalOutput.Item1;
            List<LineContext> context = lexicalOutput.Item2;

            ASTNode ast = (new SyntaxAnalyzer()).CheckSyntax(tokens, context);

            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            List<Table> symbolTables = semanticAnalyzer.GenerateSymbolTables(ast);
            semanticAnalyzer.CheckSemantic(ast);

            interCode = (new IntermediateCodeGenerator()).GenerateCode(source, output, ast, symbolTables);

            interCode = CodeOptimizer.OptimizeCode(interCode);

            string result = CodeGenerator.MakeAssembly(output, interCode);

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
