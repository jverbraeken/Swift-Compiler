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
            Console.WriteLine("Swift Compiler by Joost Verbraeken");
            string[] text = System.IO.File.ReadAllLines(args[0]);

            List<Lexeme> lexemes = LexicalAnalyzer.GetLexemes(text);
            foreach (Lexeme line in lexemes)
            {
                Console.WriteLine(line.line);
            }
            CodeEmitter.MakeAssembly(args[0], args[1]);

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
