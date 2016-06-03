using Swift.Phrases;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift.AST_Nodes
{
    public class StringLiteral : Literal, Exp
    {
        public StringLiteral(ILineContext context, string value) : base(context, value)
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
