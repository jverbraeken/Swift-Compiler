using System;
using Swift.AST_Nodes;
using Swift.Phrases;
using Swift.Tokens;
using System.Xml.Linq;

namespace Swift
{
    public class Int8Literal : Literal, Exp
    {
        public Int8Literal(ILineContext context, string value) : base(context, value)
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public ASTType accept(TypeVisitor v)
        {
            return v.visit(this);
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res = new XElement(GetType().Name, new XAttribute("Value", Value));
            XMLParser.ParseXMLProperties(this, res, prop);
            return res;
        }
    }
}
