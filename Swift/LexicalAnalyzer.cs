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
            foreach (string line_raw in input)
            {
                string line = eat_whitespace(line_raw);
                output.Add(new Lexeme(line));
            }
            return output;
        }

        static string eat_whitespace(string line)
        {
            int i = 0;

            while (true)
            {
                char c = line[i];
                if (c != ' ')
                    break;
                i++;
            }
            /*if (i == Global.MAX_LINE_LENGTH)
            {
                Swift.error("A line exceeded the maximum line length of " + Global.MAX_LINE_LENGTH.ToString() + "characters: " + line);
            }*/
            line = line.Substring(i);
            return line;
        }
    }
}
