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
        public static Global.InstructionSets architecture = Global.InstructionSets.X86_64;

        public static void Main(string[] args)
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
                        else if (args[i] == "-IS")
                            lookingFor = "instructionSet";
                        else
                            error(new InvalidArgumentException("An invalid argument was supplied: " + args[i]));
                    }
                    else
                    {
                        if (source == "")
                            source = args[i];
                        else
                            error(new MultipleSourceFilesException("The source can be supplied only once: " + args[i]));
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
                            default: error(new UnknownInstructionSetException("an unknown Instruction Set was supplied: " + args[i])); break;
                        }
                    }
                }
            }
            Console.WriteLine("Swift Compiler by Joost Verbraeken");
            string[] text = System.IO.File.ReadAllLines(source);

            Tuple<List<Token>, List<ILineContext>> lexicalOutput = (new LexicalAnalyzer()).GetTokens(text);
            List<Token> tokens = lexicalOutput.Item1;
            List<ILineContext> context = lexicalOutput.Item2;

            Base ast = (new SyntaxAnalyzer()).CheckSyntax(tokens, context, architecture);

            SemanticAnalyzer semanticAnalyzer = new SemanticAnalyzer();
            List<Table> symbolTables = semanticAnalyzer.CheckSemantics(ast);

            List<Module> modules = (new IntermediateCodeGenerator()).GenerateCode(source, output, ast, symbolTables);

            modules = CodeOptimizer.OptimizeCode(modules);
            
           string result = CodeGenerator.MakeAssembly(source, output, modules, architecture);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.ReadLine();
        }

        public static void error(ISwiftException exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (exception.Line == 0)
                Console.WriteLine("Line " + exception.Line + ", column " + exception.Pos + ": " + exception.ToString());
            else
                Console.WriteLine(exception.ToString());
            Console.ReadLine();
            throw (Exception) exception;
        }





        [Serializable()]
        public class UnknownInstructionSetException : SwiftException
        {
            public UnknownInstructionSetException(string message = "") : base(message) { }
        }

        [Serializable()]
        public class MultipleSourceFilesException : SwiftException
        {
            public MultipleSourceFilesException(string message = "") : base(message) { }
        }

        [Serializable()]
        public class InvalidArgumentException : SwiftException
        {
            public InvalidArgumentException(string message = "") : base(message) { }
        }
    }
}
