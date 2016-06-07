using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public interface ISwiftException
    {
        int Line { get; set; }
        int Pos { get; set; }
    }

    public class SwiftException : Exception, ISwiftException
    {
        public int Line { get; set; }
        public int Pos { get; set; }

        public SwiftException() : base() { Line = 0; Pos = 0; }
        public SwiftException(string message) : base(message) { Line = 0; Pos = 0; }
        public SwiftException(string message, Exception inner) : base(message, inner) { Line = 0; Pos = 0; }

        public SwiftException(int line, int pos) : base() { Line = line; Pos = pos; }
        public SwiftException(int line, int pos, string message) : base(message) { Line = line; Pos = pos; }
        public SwiftException(int line, int pos, string message, Exception inner) : base(message, inner) { Line = line; Pos = pos; }

        public SwiftException(ILineContext context) : base() { Line = context.Line; Pos = context.Pos; }
        public SwiftException(ILineContext context, string message) : base(message) { Line = context.Line; Pos = context.Pos; }
        public SwiftException(ILineContext context, string message, Exception inner) : base(message, inner) { Line = context.Line; Pos = context.Pos; }
    }
}
