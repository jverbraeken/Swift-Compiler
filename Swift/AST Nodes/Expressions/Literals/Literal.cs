using Swift.AST_Nodes;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift
{
    public abstract class Literal : ASTNode
    {
        public string Value { get; set; }

        public Literal(ILineContext context) : base(context)
        {
        }

        public Literal(ILineContext context, string value) : base(context)
        {
            Value = value;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res = new XElement(GetType().Name, new XAttribute("Value", Value));
            XMLParser.ParseXMLProperties(this, res, prop);
            return res;
        }
    }
}
