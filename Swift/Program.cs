using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    class Program
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
            Console.ReadLine();
        }
    }
}
