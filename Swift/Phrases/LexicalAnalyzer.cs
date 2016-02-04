using Swift.Tokens;
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
        internal static readonly string regexIdentifier = "[_a-z][a-z0-9]*",
        regexIdentity = "^([\\+\\-]{2,})?(" + regexIdentifier + ")([\\+\\-]{2,})?",
        //Literals
        regexBinary = "^0b([01]+)",
        regexOctal = "^0c([0-7]+)",
        regexHexadecimal = "^0x([0-9A-F]+)",
        regexDouble = "^[0-9]*\\.[0-9]+",
        regexInt = "^[0-9]+",
        regexString = "^\"[^\"]*\"",
        //Keywords
        regexVar = "^var\\(? +",
        regexLet = "^let\\(? +",
        //Punctuation
        regexOpenRoundBracket = "^\\(",
        regexCloseRoundBracket = "^\\)",
        regexOpenSquareBracket = "^\\[",
        regexCloseSquareBracket = "^\\]",
        regexOpenBraces = "^\\{",
        regexCloseBraces = "^\\}",
        //Operators
        regexOperator = "^[\\/\\=\\-\\+\\!\\*\\%\\<\\>\\%\\|\\^\\~\\?][^\\/]",
        //Comments
        regexComment = "^\\/\\/";

        int MultilineCommentLevel = 0;
        public Tuple<List<Token>, List<LineContext>> GetTokens(string[] input)
        {
            List<Token> tokens = new List<Token>();
            List<LineContext> context = new List<LineContext>();
            Match match;
            int lineX;     //The character at the line
            int lineY = 1; //The nth line as seen from the top

            foreach (string line_raw in input)
            {
                lineX = 1;
                string line = line_raw;
                while (line != "")
                {
                    string[] tmp = EatWhitespace(line);
                    line = tmp[0];
                    lineX += int.Parse(tmp[1]);


                    //Comments


                    //Single line
                    match = Regex.Match(line, regexComment);
                    if (match.Success)
                    {
                        break;
                    };


                    //Literals


                    //Integer
                    match = Regex.Match(line, regexInt);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Int, match.Groups[0].Value));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };

                    //String
                    match = Regex.Match(line, regexString);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.String, match.Groups[0].Value));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };


                    //Keywords


                    //Let
                    match = Regex.Match(line, regexLet);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Let));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };

                    //Var
                    match = Regex.Match(line, regexVar);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Var));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };


                    //Punctuation


                    //Open Round Bracket
                    match = Regex.Match(line, regexOpenRoundBracket);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Open_round_bracket));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };

                    //Close Round Bracket
                    match = Regex.Match(line, regexCloseRoundBracket);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Close_round_bracket));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };

                    //Open Square Bracket
                    match = Regex.Match(line, regexOpenSquareBracket);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Open_square_bracket));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };

                    //Close Square Bracket
                    match = Regex.Match(line, regexCloseSquareBracket);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Close_square_bracket));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };

                    //Open Braces
                    match = Regex.Match(line, regexOpenBraces);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Open_brace));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };

                    //Close Braces
                    match = Regex.Match(line, regexCloseBraces);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Close_brace));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };


                    //Operator


                    //Operator
                    match = Regex.Match(line, regexOperator);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Operator, match.Groups[0].Value));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };


                    //Identifiers
                    match = Regex.Match(line, regexIdentity);
                    if (match.Success)
                    {
                        tokens.Add(new Token(Global.DataType.Identifier, match.Groups[0].Value));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match.Length); lineX += match.Length; continue;
                    };

                    Swift.error("Syntax error: \"" + Regex.Match(line, "/^[^\\s]+").Value + "\" could not be identified", 1);
                }
                lineY++;
            }
            return Tuple.Create(tokens, context);
        }

        /// <summary>
        /// Removes the whitespace from the start of one line.
        /// This method is called many times to eat all whitespace from one line.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string[] EatWhitespace(string line)
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
            return new string[] { line, i.ToString()} ;
        }
        
        public static bool isSpace(char c)
        {
            return (c == '\u0020' || c == '\u000A' || c == '\u000D' || c == '\u0009' || c == '\u000B' || c == '\u000C' || c == '\u0000');
        }

        public static bool isKeyword(string s)
        {
            return (s == "class" || s == "deinit" || s == "enum" || s == "extension" || s == "func" || s == "import" || s == "init" || s == "inout" || s == "internal" || s == "let"
                || s == "private" || s == "protocol" || s == "public" || s == "static" || s == "struct" || s == "subscript" || s == "typealias" || s == "var"
                || s == "break" || s == "case" || s == "continue" || s == "default" || s == "defer" || s == "do" || s == "else" || s == "fallthrough"
                || s == "for" || s == "guard" || s == "if" || s == "in" || s == "repeat" || s == "return" || s == "switch" || s == "where" || s == "while"
                || s == "as" || s == "catch" || s == "dynamictype" || s == "is" || s == "nil" || s == "rethrows" || s == "super" || s == "self"
                || s == "throw" || s == "throws" || s == "try" || s == "__COLUMN__" || s == "__FILE__" || s == "__FUNCTION__" || s == "__LINE__"
                || s == "Int8" || s == "UInt8" || s == "Int16" || s == "UInt16" || s == "Int32" || s == "Uint32" || s == "Int64" || s == "UInt64" || s == "Float" || s == "Double");
        }

        /*     public static void SetKeyword(Token tok, string lexeme)
             {
                 switch (lexeme)
                 {
                     case "class": tok.type = Global.DataType.Class; break;
                     case "deinit": tok.type = Global.DataType.Deinit; break;
                     case "enum": tok.type = Global.DataType.Class; break;
                     case "extension": tok.type = Global.DataType.Class; break;
                     case "func": tok.type = Global.DataType.Class; break;
                     case "import": tok.type = Global.DataType.Class; break;
                     case "init": tok.type = Global.DataType.Class; break;
                     case "inout": tok.type = Global.DataType.Class; break;
                     case "internal": tok.type = Global.DataType.Class; break;
                     case "let": tok.type = Global.DataType.Class; break;
                         //Todo
                     default: Swift.error("Internal compliation error: Lexical Analyzer : trying to set keyword to " + lexeme, 1); break;
                 }
             }*/

        public static bool isPunctuation(string s)
        {
            return (s == "(" || s == ")" || s == "[" || s == "]" || s == "{" || s == "}" || s == "," || s == ":" || s == ";" || s == "@" || s == "#" || s == "`");
        }
        /*
                public static void SetPunctuation(Token tok, string lexeme)
                {
                    switch (lexeme)
                    {
                        case "(": tok.type = Global.DataType.Open_round_bracket; break;
                        case ")": tok.type = Global.DataType.Close_round_bracket; break;
                        case "[": tok.type = Global.DataType.Open_square_bracket; break;
                        case "]": tok.type = Global.DataType.Close_square_bracket; break;
                        case "{": tok.type = Global.DataType.Open_brace; break;
                        case "}": tok.type = Global.DataType.Close_brace; break;
                        case ",": tok.type = Global.DataType.Comma; break;
                        case ";": tok.type = Global.DataType.Semicolon; break;
                        case ":": tok.type = Global.DataType.Colon; break;
                        case "@": tok.type = Global.DataType.At; break;
                        case "#": tok.type = Global.DataType.Hashtag; break;
                        case "~": tok.type = Global.DataType.Accent_grave; break;
                        //Todo
                        default: Swift.error("Internal compliation error: Lexical Analyzer : trying to set punctuation to " + lexeme, 1); break;
                    }
                }*/

        public static bool isOperator(string s)
        {
            return (s == "/" || s == "=" || s == "-" || s == "+" || s == "!" || s == "*" || s == "%" || s == "<" || s == ">" || s == "&" || s == "|" || s == "^" || s == "?" || s == "~");
            /*Division, Equals, Minus, Plus, Exclamation, Multiplication, Percentage, Less, More, And, Or, Caret, Tilde, Question, Open_round_bracket, Close_round_bracket, Open_square_bracket, Close_square_bracket, Open_brace, Close_brace, Dot, Point, Colon, Semicolon, At, Hashtag, Single_quotation, Double_quotation, Slash, Backslash*/
        }

        /*     public static void SetOperator(Token tok, string lexeme)
             {
                 switch (lexeme)
                 {
                     case "/": tok.type = Global.DataType.Division; break;
                     case "=": tok.type = Global.DataType.Equals; break;
                     case "-": tok.type = Global.DataType.Minus; break;
                     case "+": tok.type = Global.DataType.Plus; break;
                     case "!": tok.type = Global.DataType.Exclamation; break;
                     case "*": tok.type = Global.DataType.Multiplication; break;
                     case "%": tok.type = Global.DataType.Percentage; break;
                     case "<": tok.type = Global.DataType.Less; break;
                     case ">": tok.type = Global.DataType.More; break;
                     case "&": tok.type = Global.DataType.And; break;
                     case "|": tok.type = Global.DataType.Or; break;
                     case "^": tok.type = Global.DataType.Caret; break;
                     case "?": tok.type = Global.DataType.Question; break;
                     case "~": tok.type = Global.DataType.Tilde; break;
                     //Todo
                     default: Swift.error("Internal compliation error: Lexical Analyzer : trying to set operator to " + lexeme, 1); break;
                 }
             }
             */
        public static bool isLiteral(string s)
        {
            return (s[0] == '"' || s == "true" || s == "false" || Regex.Match(s, @"[0-9]*\.?[0-9]+").Value == s);
        }
    }
}
