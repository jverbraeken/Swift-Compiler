using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            , BINARY, OCTAL, HEXADECIMAL, INT, STRING, DOUBLE, STRINGINTERPOLATION, STRINGINTERPOLATIONEND, ESCAPEDCHARACTER
            , INTTYPE, INT8TYPE, INT16TYPE, INT32TYPE, INT64TYPE, UINTTYPE, UINT8TYPE, UINT16TYPE, UINT32TYPE, UINT64TYPE, FLOATTYPE, DOUBLETYPE, BOOLTYPE, STRINGTYPE, CHARACTERTYPE, OINTTYPE, OINT8TYPE, OINT16TYPE, OINT32TYPE, OINT64TYPE, OUINTTYPE, OUINT8TYPE, OUINT16TYPE, OUINT32TYPE, OUINT64TYPE,  OFLOATTYPE, ODOUBLETYPE, OBOOLTYPE, OSTRINGTYPE, OCHARACTERTYPE, VOID //The 'O' of optional
            , OPERATOR
            , OPEN_ROUND_BRACKET, CLOSE_ROUND_BRACKET, OPEN_SQUARE_BRACKET, CLOSE_SQUARE_BRACKET, OPEN_BRACE, CLOSE_BRACE, COMMA, COLON, SEMICOLON, AT, HASHTAG, SINGLE_QUOTATION, DOUBLE_QUOTATION, ACCENT_GRAVE, UNDERSCORE
            , BUILTIN_FUNC, BUILTIN_CONSTANT
            , ENDSTATEMENT
        }

        public enum Registers
        {
            STACKBASEPOINTER, STACKPOINTER, INSTRUCTIONPOINTER, ACCUMULATOR, BASE, COUNTER, DATA, DEST_INDEX, SRC_INDEX
        }

        public enum InstructionSets
        {
            X86, X86_64
        }

        public enum Associativity
        {
            LEFT, RIGHT, NONE
        }

        public enum Scope
        {
            BuiltinScope, MainScope, ClassScope, MethodScope
        }

        public enum ElementTypes
        {
            quotedTextItem, interpolation, escapedCharacter
        }

        public enum EscapedCharacter
        {
            Null, Backslash, HorizontalTab, LineFeed, CarriageReturn, DoubleQuote, SingleQuote
        }
    }
}
