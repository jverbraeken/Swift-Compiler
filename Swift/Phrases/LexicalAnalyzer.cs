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
        private int lineX;     //The character at the line
        private int lineY = 1; //The nth line as seen from the top
        private int multilineCommentLevel = 0;
        private List<Token> tokens = new List<Token>();
        private List<ILineContext> context = new List<ILineContext>();
        internal static readonly string regexIdentifier = "[_a-zA-Z_][a-zA-Z_0-9]*",
        regexIdentity = "^([\\+\\-]{2,})?(" + regexIdentifier + ")([\\+\\-]{2,})?",
        //Literals
        regexBinary = "^0b([01]+)",
        regexOctal = "^0c([0-7]+)",
        regexHexadecimal = "^0x([0-9A-F]+)",
        regexDouble = "^[0-9]*\\.[0-9]+",
        regexInt = "^[0-9]+",
        regexString = "^\"[^\"]*\"",
        regexStringInterpolation = @"\\\(",
        regexStringInterpolationAtStart = @"^\\\(",
        //Keywords
        regexVar = "^var\\(? +",
        regexLet = "^let\\(? +",
        regexBoolType = "^\\bBool\\b",
        regexCharType = "^\\bChar\\b",
        regexDoubleType = "^\\bDouble\\b",
        regexFloatType = "^\\bFloat\\b",
        regexInt16Type = "^\\bInt16\\b",
        regexInt32Type = "^\\bInt32\\b",
        regexInt64Type = "^\\bInt64\\b",
        regexInt8Type = "^\\bInt8\\b",
        regexStringType = "^\\bString\\b",
        regexUInt16Type = "^\\bUInt16\\b",
        regexUInt32Type = "^\\bUInt32\\b",
        regexUInt64Type = "^\\bUInt64\\b",
        regexUInt8Type = "^\\bUInt8\\b",
        regexOBoolType = "^\\bBool\\?\\b",
        regexOCharType = "^\\bChar\\?\\b",
        regexODoubleType = "^\\bDouble\\?\\b",
        regexOFloatType = "^\\bFloat\\?\\b",
        regexOInt16Type = "^\\bInt16\\?\\b",
        regexOInt32Type = "^\\bInt32\\?\\b",
        regexOInt64Type = "^\\bInt64\\?\\b",
        regexOInt8Type = "^\\bInt8\\?\\b",
        regexOStringType = "^\\bString\\?\\b",
        regexOUInt16Type = "^\\bUInt16\\?\\b",
        regexOUInt32Type = "^\\bUInt32\\?\\b",
        regexOUInt64Type = "^\\bUInt64\\?\\b",
        regexOUInt8Type = "^\\bUInt8\\?\\b",
        //Punctuation
        regexOpenRoundBracket = "^\\(",
        regexCloseRoundBracket = "^\\)",
        regexOpenSquareBracket = "^\\[",
        regexCloseSquareBracket = "^\\]",
        regexOpenBraces = "^\\{",
        regexCloseBraces = "^\\}",
        //Operators
        regexOperator = "^[\\/\\=\\-\\+\\!\\*\\%\\<\\>\\&\\|\\^\\~\\?]",
        regexComma = "^\\,",
        regexColon = "^: +",
        regexUnderscore = "^_\\s+",
        //Comments
        regexComment = "^\\/\\/";

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
            return new string[] { line, i.ToString() };
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





        public Tuple<List<Token>, List<ILineContext>> GetTokens(string[] input)
        {
            foreach (string line_raw in input)
            {
                lineX = 1;
                string line = line_raw;
                evaluateString(line);
                tokens.Add(new Token(Global.DataType.ENDSTATEMENT, ""));
                context.Add(new LineContext(lineX, lineY));
                lineY++;
            }
            return Tuple.Create(tokens, context);
        }

        public void evaluateString(string line)
        {
            bool shouldContinue;
            while (line != "")
            {
                shouldContinue = evaluateFirstToken(ref line);
                if (!shouldContinue)
                    break;
            }
        }

        /// <summary>
        /// Evaluates the first token of line
        /// </summary>
        /// <param name="line">A string of which the first token will be evaluate (and possibly others in special cases like string interpolation)</param>
        /// <returns>False if a comment is found and thus the sentence should not be evaluated any further</returns>
        public bool evaluateFirstToken(ref string line)
        {

            Match match;
            string[] tmp = EatWhitespace(line);
            line = tmp[0];
            lineX += int.Parse(tmp[1]);


            //Comments


            //Single line
            match = Regex.Match(line, regexComment);
            if (match.Success)
            {
                return false;
            };


            //Literals


            //Binary
            match = Regex.Match(line, regexBinary);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.BINARY, match.Groups[0].Value));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Octal
            match = Regex.Match(line, regexOctal);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OCTAL, match.Groups[0].Value));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Hexadecimal
            match = Regex.Match(line, regexHexadecimal);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.HEXADECIMAL, match.Groups[0].Value));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Double
            match = Regex.Match(line, regexDouble);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.DOUBLE, match.Groups[0].Value));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Integer
            match = Regex.Match(line, regexInt);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.INT, match.Groups[0].Value));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //String
            match = Regex.Match(line, regexString);
            if (match.Success)
            {
                line = line.Substring(1); // Remove the '"'
                while (true)
                {
                    Match match2 = Regex.Match(line, regexStringInterpolation);
                    if (match2.Success) // We found a string interpolation \(
                    {
                        tokens.Add(new Token(Global.DataType.STRING, line.Substring(0, match2.Index)));
                        context.Add(new LineContext(lineX, lineY));
                        line = line.Substring(match2.Index); // Now the string interpolation is at the start of line
                        bool shouldContinue = evaluateFirstToken(ref line);
                        if (!shouldContinue)
                            Swift.error(new StringInterpolationWithoutEndException(lineX, lineY, "Error in lexical analysis: cannot find the end of the string interpolation"));
                        if (line[0] == '"')
                            break;
                    }
                    else
                    {
                        if (line != "")
                        {
                            tokens.Add(new Token(Global.DataType.STRING, line.Remove(line.Count() - 1, 1))); // Remove the '"' at the end of the string as well
                            context.Add(new LineContext(lineX, lineY));
                            line = line.Substring(match.Length); // Remove the string from 
                            lineX += match.Length;
                        }
                        break;
                    }
                }
                return true;
            };

            // String interpolation (used when the string interpolation happens not at the start of a sentence
            match = Regex.Match(line, regexStringInterpolationAtStart);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.STRINGINTERPOLATION, null));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(2); // Remove the string interpolation
                int level = 1;
                bool shouldContinue;
                int firstSize = line.Count();
                Token lastToken;
                while (true)
                {
                    shouldContinue = evaluateFirstToken(ref line);
                    if (!shouldContinue)
                        Swift.error(new StringInterpolationWithoutEndException(lineX, lineY, "Error in lexical analysis: cannot find the end of the string interpolation"));
                    lastToken = tokens[tokens.Count - 1];
                    if (lastToken.type == Global.DataType.OPEN_ROUND_BRACKET)
                        level++;
                    else if (lastToken.type == Global.DataType.CLOSE_ROUND_BRACKET)
                        level--;
                    if (level == 0) // We found the finishing ')'
                    {
                        tokens.RemoveAt(tokens.Count - 1); // Remove the last token ')' because this isn't needed anymore as we replace it with STRINGINTERPOLATIONEND
                        context.RemoveAt(context.Count - 1);
                        tokens.Add(new Token(Global.DataType.STRINGINTERPOLATIONEND, null));
                        context.Add(new LineContext(lineX, lineY));
                        break;
                    }
                }
                return true;
            }


                //Keywords


                //Let
                match = Regex.Match(line, regexLet);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.LET));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Var
            match = Regex.Match(line, regexVar);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.VAR));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //BoolType
            match = Regex.Match(line, regexBoolType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.BOOLTYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //CharType
            match = Regex.Match(line, regexCharType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.CHARACTERTYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //DoubleType
            match = Regex.Match(line, regexDoubleType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.DOUBLETYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //FloatType
            match = Regex.Match(line, regexFloatType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.FLOATTYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Int8Type
            match = Regex.Match(line, regexInt8Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.INT8TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Int16Type
            match = Regex.Match(line, regexInt16Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.INT16TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Int32Type
            match = Regex.Match(line, regexInt32Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.INT32TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Int64Type
            match = Regex.Match(line, regexInt64Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.INT64TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //StringType
            match = Regex.Match(line, regexStringType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.STRINGTYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //UInt8Type
            match = Regex.Match(line, regexUInt8Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.UINT8TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //UInt16Type
            match = Regex.Match(line, regexUInt16Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.UINT16TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //UInt32Type
            match = Regex.Match(line, regexUInt32Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.UINT32TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //UInt64Type
            match = Regex.Match(line, regexUInt64Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.UINT64TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            ///////////         OPTIONALS


            //OBoolType
            match = Regex.Match(line, regexOBoolType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OBOOLTYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OCharType
            match = Regex.Match(line, regexOCharType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OCHARACTERTYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //ODoubleType
            match = Regex.Match(line, regexODoubleType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.ODOUBLETYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OFloatType
            match = Regex.Match(line, regexOFloatType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OFLOATTYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OInt8Type
            match = Regex.Match(line, regexOInt8Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OINT8TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OInt16Type
            match = Regex.Match(line, regexOInt16Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OINT16TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OInt32Type
            match = Regex.Match(line, regexOInt32Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OINT32TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OInt64Type
            match = Regex.Match(line, regexOInt64Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OINT64TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OStringType
            match = Regex.Match(line, regexOStringType);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OSTRINGTYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OUInt8Type
            match = Regex.Match(line, regexOUInt8Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OUINT8TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OUInt16Type
            match = Regex.Match(line, regexOUInt16Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OUINT16TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OUInt32Type
            match = Regex.Match(line, regexOUInt32Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OUINT32TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //OUInt64Type
            match = Regex.Match(line, regexOUInt64Type);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OUINT64TYPE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };





            //Punctuation


            //Open Round Bracket
            match = Regex.Match(line, regexOpenRoundBracket);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OPEN_ROUND_BRACKET));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Close Round Bracket
            match = Regex.Match(line, regexCloseRoundBracket);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.CLOSE_ROUND_BRACKET));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Open Square Bracket
            match = Regex.Match(line, regexOpenSquareBracket);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OPEN_SQUARE_BRACKET));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Close Square Bracket
            match = Regex.Match(line, regexCloseSquareBracket);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.CLOSE_SQUARE_BRACKET));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Open Braces
            match = Regex.Match(line, regexOpenBraces);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OPEN_BRACE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Close Braces
            match = Regex.Match(line, regexCloseBraces);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.CLOSE_BRACE));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };


            //Operator


            //Operator
            match = Regex.Match(line, regexOperator);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.OPERATOR, match.Groups[0].Value));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Comma
            match = Regex.Match(line, regexComma);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.COMMA));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Annotation
            match = Regex.Match(line, regexColon);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.COLON, ":"));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Annotation
            match = Regex.Match(line, regexUnderscore);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.UNDERSCORE, "_"));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };

            //Identifiers
            match = Regex.Match(line, regexIdentity);
            if (match.Success)
            {
                tokens.Add(new Token(Global.DataType.IDENTIFIER, match.Groups[0].Value));
                context.Add(new LineContext(lineX, lineY));
                line = line.Substring(match.Length); lineX += match.Length; return true;
            };


            Swift.error(new UndentifiedTokenException(lineX, lineY, "\"" + line + "\" could not be identified"));
            return false;
        }



        [Serializable()]
        public class StringInterpolationWithoutEndException : SwiftException
        {
            public StringInterpolationWithoutEndException(int line, int pos, string message = "end of string interpolation not found") : base(line, pos, message) { }
        }

        [Serializable()]
        public class UndentifiedTokenException : SwiftException
        {
            public UndentifiedTokenException(int line, int pos, string message = "") : base(line, pos, message) { }
        }
    }
}
