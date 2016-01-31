using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    class LexicalAnalyzer
    {
        public static List<Token> GetLexemes(string[] input)
        {
            List<Token> output = new List<Token>();
            foreach (string line_raw in input)
            {
                List<string> lexemes = stringToLexemes(line_raw);
                foreach (string lexeme in lexemes)
                {
                    if (isFunction(lexeme))
                        output.Add(new Token(lexeme));
                }
            }
            return output;
        }

        static string eat_whitespace(string line)
        {
            int i = 0;

            while (true)
            {
                char c = line[i];
                if (!isSpace(c))
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

        static List<string> stringToLexemes(string lineIn)
        {
            string line = lineIn;
            List<string> output = new List<string>();
            while (true)
            {
                line = eat_whitespace(line);
                int i = 0;
                while (true)
                {
                    if (line[i] != ' ')
                        i++;
                    else
                        break;
                }
                output.Add(line.Substring(0, i));
                line = line.Substring(i);
            }
        }

        static bool isSpace(char c)
        {
            return (c == '\u0020' || c == '\u000A' || c == '\u000D' || c == '\u0009' || c == '\u000B' || c == '\u000C' || c == '\u0000');
        }
    }
}
