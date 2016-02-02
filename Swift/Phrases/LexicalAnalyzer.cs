using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Swift
{
    public class LexicalAnalyzer
    {
        static int MultilineCommentLevel = 0;
        public static List<Token> GetLexemes(string[] input)
        {
            List<Token> output = new List<Token>();

            foreach (string line_raw in input)
            {
                List<string> lexemes = StringToLexemes(line_raw);
                foreach (string lexeme in lexemes)
                {
                    if (isKeyword(lexeme))
                        output.Add(new Token(Global.PrimitiveType.KEYWORD, lexeme));
                    if (isPunctuation(lexeme))
                        output.Add(new Token(Global.PrimitiveType.PUNCTUATION, lexeme));
                    if (isOperator(lexeme))
                        output.Add(new Token(Global.PrimitiveType.OPERATOR, lexeme));
                    if (isLiteral(lexeme))
                        output.Add(new Token(Global.PrimitiveType.LITERAL, lexeme));
                }
            }
            return output;
        }

        /// <summary>
        /// Removes the whitespace from the start of one line.
        /// This method is called many times to eat all whitespace from one line.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string EatWhitespace(string line)
        {
            int i = 0;
            int cap = 0;

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
            if (cap == 0)
                line = line.Substring(i);
            else
                line = line.Substring(i, cap);
            return line;
        }

        public static List<string> StringToLexemes(string lineIn)
        {
            string line = lineIn;
            List<string> output = new List<string>();
            Boolean comment = false;
            Boolean str = false;
            while (true)
            {
                line = EatWhitespace(line);
                int i = 0;
                if (line[i] == '"')
                {
                    str = true;
                    i++;
                }
                while (true)
                {
                    if (str)
                    {
                        if (line[i] != '"')
                            i++;
                        else {
                            i++;
                            str = false;
                            break;
                        }
                    }
                    else {
                        if (line.Length > i + 1)
                        {
                            if (line[i] == '/' && line[i + 1] == '/') // A comment starts
                            {
                                comment = true;
                                i = line.Length;
                                break;
                            }
                            if (line[i] == '/' && line[i + 1] == '*') // A new multiline comment starts
                                MultilineCommentLevel++;
                            if (line[i] == '*' && line[i + 1] == '/') // A multiline comment ends
                            {
                                MultilineCommentLevel--;
                                if (MultilineCommentLevel < 0)
                                {
                                    Swift.error("You cannot end a multiline comment you didn't start: " + line, 1);
                                }
                                line = line.Substring(i + 2);
                                i = 0;
                                break;
                            }
                        }
                        if (MultilineCommentLevel > 0)
                            i++;
                        if (!comment && MultilineCommentLevel == 0)
                        {
                            if (isSpace(line[i]))
                                break;
                            else if (isPunctuation(line[i].ToString()) || isOperator(line[i].ToString()))
                            {
                                if (i > 0)
                                    output.Add(line.Substring(0, i));
                                line = line.Substring(i);
                                i = 1;
                                break;
                            }
                            else
                                i++;
                        }
                    }
                    if (i >= line.Length)
                        break;
                }
                if (!comment && MultilineCommentLevel == 0 && line.Substring(0, i).Length > 0)
                    output.Add(line.Substring(0, i));
                line = line.Substring(i);
                if (line.Length == 0)
                    break;
            }
            return output;
        }

        public static bool isSpace(char c)
        {
            return (c == '\u0020' || c == '\u000A' || c == '\u000D' || c == '\u0009' || c == '\u000B' || c == '\u000C' || c == '\u0000');
        }

        public static bool isKeyword(string s)
        {
            return (s == "Class" || s == "Deinit" || s == "Enum" || s == "Extension" || s == "Func" || s == "Import" || s == "Init" || s == "Inout" || s == "Internal" || s == "Let" || s == "Private");
        /*        Class, Deinit, Enum, Extension, Func, Import, Init, Inout, Internal, Let, Private, Protocol, Public, Static, Struct, Subscript, Typealias, Var
            , Break, Case, Continue, Default, Defer, Do, Else, Fallthrough, For, Guard, If, In, Repeat, Return, Switch, Where, While
            , As, Catch, DynamicType, False, Is, Nil, Rethrows, Super, Self, Throw, Throws, True, Try, __COLUMN__, __FILE__, __FUNCTION__, __LINE__*/
        }

        public static bool isPunctuation(string s)
        {
            return (s == "(" || s == ")" || s == "[" || s == "]" || s == "{" || s == "}" || s == "," || s == ":" || s == ";" || s == "=" || s == "@" || s == "#" || s == "&" || s == "`" || s == "?" || s == "!");
        }

        public static bool isOperator(string s)
        {
            return (s == "/" || s == "=" || s == "-" || s == "+" || s == "!" || s == "*" || s == "%" || s == "<" || s == ">" || s == "&" || s == "|" || s == "^" || s == "?" || s == "~");
            /*Division, Equals, Minus, Plus, Exclamation, Multiplication, Percentage, Less, More, And, Or, Caret, Tilde, Question, Open_round_bracket, Close_round_bracket, Open_square_bracket, Close_square_bracket, Open_brace, Close_brace, Dot, Point, Colon, Semicolon, At, Hashtag, Single_quotation, Double_quotation, Slash, Backslash*/
        }

        public static bool isLiteral(string s)
        {
            return (s[0] == '"' || s == "true" || s == "false" || Regex.Match(s, @"[0-9]*\.?[0-9]+").Value == s);
        }
    }
}
