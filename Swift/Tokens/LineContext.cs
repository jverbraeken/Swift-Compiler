using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift.Tokens
{
    public interface ILineContext
    {
        int Pos { get; }
        int Line { get; }
        XElement ToXML();
    }

    public class LineContext : ILineContext
    {
        public int Pos { get; internal set; } //position in the line
        public int Line { get; internal set; } //line in the source code

        public LineContext(int pos, int line)
        {
            Pos = pos;
            Line = line;
        }

        public XElement ToXML()
        {
            return new XElement(GetType().Name, new XAttribute[] { new XAttribute("pos", Pos), new XAttribute("line", Line) });
        }
    }
}
