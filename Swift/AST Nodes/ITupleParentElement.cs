using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift.AST_Nodes
{
    public interface ITupleParentElement
    {
        ILineContext Context { get; set; }
        Table Scope { get; set; }
        XElement ToXML(XMLParser.XMLProperties prop);
    }
}
