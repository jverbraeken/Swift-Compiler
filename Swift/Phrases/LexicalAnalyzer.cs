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
        regexOperator = "^[\\/\\=\\-\\+\\!\\*\\%\\<\\>\\%\\|\\^\\~\\?]",
        regexComma = "^\\,",
        regexColon = "^: +",
        regexUnderscore = "^_\\s+",
        //Comments
        regexComment = "^\\/\\/";
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
            while (line != "")
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
                    break;
                };


                //Literals


                //Binary
                match = Regex.Match(line, regexBinary);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.BINARY, match.Groups[0].Value));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Octal
                match = Regex.Match(line, regexOctal);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OCTAL, match.Groups[0].Value));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Hexadecimal
                match = Regex.Match(line, regexHexadecimal);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.HEXADECIMAL, match.Groups[0].Value));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Double
                match = Regex.Match(line, regexDouble);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.DOUBLE, match.Groups[0].Value));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Integer
                match = Regex.Match(line, regexInt);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.INT, match.Groups[0].Value));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //String
                match = Regex.Match(line, regexString);
                if (match.Success)
                {
                    string str = match.Groups[0].Value.Substring(1, match.Groups[0].Value.Length - 2);
                    while (true)
                    {
                        if (str == "")
                        {
                            line = line.Substring(match.Length);
                            lineX += match.Length;
                            break;
                        }
                        Match match2 = Regex.Match(str, regexStringInterpolation);
                        if (match2.Success) // We found a string interpolation \(
                        {
                            int level = 1;
                            int pos = 2 + match2.Index;
                            while (true)
                            {
                                if (pos >= str.Length)
                                    Swift.error("Error in lexical analysis: cannot find the end of the string interpolation on line " + lineX + ", column " + lineY, 1);
                                if (str[pos] == '(')
                                    level++;
                                else if (str[pos] == ')')
                                    level--;
                                if (level == 0) // We found the finishing )
                                {
                                    if (match2.Index > 0) // Add the string before the string interpolation starts
                                    {
                                        tokens.Add(new Token(Global.DataType.STRING, str.Substring(0, match2.Index)));
                                        context.Add(new LineContext(lineX, lineY));
                                    }
                                    // Add the string interpolation
                                    tokens.Add(new Token(Global.DataType.STRINGINTERPOLATION, null));
                                    context.Add(new LineContext(lineX, lineY));
                                    evaluateString(str.Substring(match2.Index + 2, pos - match2.Index - 2));
                                    tokens.Add(new Token(Global.DataType.STRINGINTERPOLATIONEND, null));
                                    context.Add(new LineContext(lineX, lineY));
                                    str = str.Substring(pos + 1);
                                    break;
                                }
                                pos++;
                            }
                        }
                        else
                        {
                            if (str != "")
                            {
                                tokens.Add(new Token(Global.DataType.STRING, str));
                                context.Add(new LineContext(lineX, lineY));
                                line = line.Substring(match.Length);
                                lineX += match.Length;
                            }
                            break;
                        }
                    }
                    continue;
                };


                //Keywords


                //Let
                match = Regex.Match(line, regexLet);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.LET));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Var
                match = Regex.Match(line, regexVar);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.VAR));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //BoolType
                match = Regex.Match(line, regexBoolType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.BOOLTYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //CharType
                match = Regex.Match(line, regexCharType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.CHARACTERTYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //DoubleType
                match = Regex.Match(line, regexDoubleType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.DOUBLETYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //FloatType
                match = Regex.Match(line, regexFloatType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.FLOATTYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Int8Type
                match = Regex.Match(line, regexInt8Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.INT8TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Int16Type
                match = Regex.Match(line, regexInt16Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.INT16TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Int32Type
                match = Regex.Match(line, regexInt32Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.INT32TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Int64Type
                match = Regex.Match(line, regexInt64Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.INT64TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //StringType
                match = Regex.Match(line, regexStringType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.STRINGTYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //UInt8Type
                match = Regex.Match(line, regexUInt8Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.UINT8TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //UInt16Type
                match = Regex.Match(line, regexUInt16Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.UINT16TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //UInt32Type
                match = Regex.Match(line, regexUInt32Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.UINT32TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //UInt64Type
                match = Regex.Match(line, regexUInt64Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.UINT64TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                ///////////         OPTIONALS


                //OBoolType
                match = Regex.Match(line, regexOBoolType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OBOOLTYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OCharType
                match = Regex.Match(line, regexOCharType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OCHARACTERTYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //ODoubleType
                match = Regex.Match(line, regexODoubleType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.ODOUBLETYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OFloatType
                match = Regex.Match(line, regexOFloatType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OFLOATTYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OInt8Type
                match = Regex.Match(line, regexOInt8Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OINT8TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OInt16Type
                match = Regex.Match(line, regexOInt16Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OINT16TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OInt32Type
                match = Regex.Match(line, regexOInt32Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OINT32TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OInt64Type
                match = Regex.Match(line, regexOInt64Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OINT64TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OStringType
                match = Regex.Match(line, regexOStringType);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OSTRINGTYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OUInt8Type
                match = Regex.Match(line, regexOUInt8Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OUINT8TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OUInt16Type
                match = Regex.Match(line, regexOUInt16Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OUINT16TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OUInt32Type
                match = Regex.Match(line, regexOUInt32Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OUINT32TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //OUInt64Type
                match = Regex.Match(line, regexOUInt64Type);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OUINT64TYPE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };





                //Punctuation


                //Open Round Bracket
                match = Regex.Match(line, regexOpenRoundBracket);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OPEN_ROUND_BRACKET));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Close Round Bracket
                match = Regex.Match(line, regexCloseRoundBracket);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.CLOSE_ROUND_BRACKET));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Open Square Bracket
                match = Regex.Match(line, regexOpenSquareBracket);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OPEN_SQUARE_BRACKET));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Close Square Bracket
                match = Regex.Match(line, regexCloseSquareBracket);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.CLOSE_SQUARE_BRACKET));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Open Braces
                match = Regex.Match(line, regexOpenBraces);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OPEN_BRACE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Close Braces
                match = Regex.Match(line, regexCloseBraces);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.CLOSE_BRACE));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };


                //Operator


                //Operator
                match = Regex.Match(line, regexOperator);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.OPERATOR, match.Groups[0].Value));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Comma
                match = Regex.Match(line, regexComma);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.COMMA));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Annotation
                match = Regex.Match(line, regexColon);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.COLON, ":"));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Annotation
                match = Regex.Match(line, regexUnderscore);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.UNDERSCORE, "_"));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };

                //Identifiers
                match = Regex.Match(line, regexIdentity);
                if (match.Success)
                {
                    tokens.Add(new Token(Global.DataType.IDENTIFIER, match.Groups[0].Value));
                    context.Add(new LineContext(lineX, lineY));
                    line = line.Substring(match.Length); lineX += match.Length; continue;
                };


                Swift.error("Syntax error: \"" + line + "\" could not be identified", 1);
            }
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
    }
}
