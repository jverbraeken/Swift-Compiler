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
        public enum DataType { IDENTIFIER, CLASS, DEINIT, ENUM, EXTENSION, FUNC, IMPORT, INIT, INOUT, INTERNAL, LET, PRIVATE, PROTOCOL, PUBLIC, STATIC, STRUCT, SUBSCRIPT, TYPEALIAS, VAR /*IDENTIFIERS*/
            , BREAK, CASE, CONTINUE, DEFAULT, DEFER, DO, ELSE, FALLTHROUGH, FOR, GUARD, IF, IN, REPEAT, RETURN, SWITCH, WHERE, WHILE
            , AS, CATCH, DYNAMICTYPE, FALSE, IS, NIL, RETHROWS, SUPER, SELF, THROW, THROWS, TRUE, TRY, __COLUMN__, __FILE__, __FUNCTION__, __LINE__
            , INT, UINT, FLOAT, DOUBLE, BOOL, STRING, CHARACTER, OINT, OUINT, OFLOAT, ODOUBLE, OBOOL, OSTRING, OCHARACTER, VOID //The 'O' of optional
            , OPERATOR
            , OPEN_ROUND_BRACKET, CLOSE_ROUND_BRACKET, OPEN_SQUARE_BRACKET, CLOSE_SQUARE_BRACKET, OPEN_BRACE, CLOSE_BRACE, COMMA, COLON, SEMICOLON, AT, HASHTAG, SINGLE_QUOTATION, DOUBLE_QUOTATION, ACCENT_GRAVE
            , BUILTIN_FUNC, BUILTIN_CONSTANT
            , ENDSTATEMENT
        }

        public enum ASTType
        {
            BASE, FUNCTION_CALL, VAR_DECLARATION, CONST_DECLARATION, STRING, ASSIGNMENT
        }

        public enum Registers
        {
            BASEPOINTER, STACKPOINTER, RAX, RDX
        }
    }
}
