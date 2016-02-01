using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public static class Global
    {
        //public static int MAX_LINE_LENGTH = 10;
        public enum Token { }
        public enum PrimitiveType { IDENTIFIER, KEYWORD, PUNCTUATION, LITERAL, OPERATOR }
        public enum DataType { Class, Deinit, Enum, Extension, Func, Import, Init, Inout, Internal, Let, Private, Protocol, Public, Static, Struct, Subscript, Typealias, Var /*Identifiers*/
            , Break, Case, Continue, Default, Defer, Do, Else, Fallthrough, For, Guard, If, In, Repeat, Return, Switch, Where, While
            , As, Catch, DynamicType, False, Is, Nil, Rethrows, Super, Self, Throw, Throws, True, Try, __COLUMN__, __FILE__, __FUNCTION__, __LINE__
            , Int, UInt, Float, Double, Bool, String, Character, Optional
            , Division, Equals, Minus, Plus, Exclamation, Multiplication, Percentage, Less, More, And, Or, Caret, Tilde, Question
            , Open_round_bracket, Close_round_bracket, Open_square_bracket, Close_square_bracket, Open_brace, Close_brace, Dot, Point, Colon, Semicolon, At, Hashtag, Single_quotation, Double_quotation, Accent_grave
        }
        public enum TypeSpecification
        {
            
        }
        //public enum DataType { Int, UInt, Float, Double, Bool, String, Character, Optional }
    }
}
