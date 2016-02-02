using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    class Swift
    {
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

            List<Token> tokens = LexicalAnalyzer.GetTokens(text);
            SyntaxAnalyzer.CheckSyntax(tokens);
            List<Table> symbolTables = SemanticAnalyzer.GenerateSymbolTables(tokens);
            SemanticAnalyzer.CheckSemantic(tokens);
            List<string> interCode = IntermediateCodeGenerator.GenerateCode(tokens, symbolTables);
            interCode = CodeOptimizer.OptimizeCode(interCode);
            CodeGenerator.MakeAssembly(source, output, intercode);

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
