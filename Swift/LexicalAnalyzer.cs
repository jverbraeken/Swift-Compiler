using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    class LexicalAnalyzer
    {
        public static List<Lexeme> GetLexemes(string[] input)
        {
            List<Lexeme> output = new List<Lexeme>();
            foreach (string line in input)
            {
                output.Add(new Lexeme(line));
            }
            return output;
        }
    }
}
