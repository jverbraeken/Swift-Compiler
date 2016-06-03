using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift.AST_Nodes
{
    public class Assignment : ASTNode
    {
        public Identifier LHS { get; set; }
        public Exp RHS { get; set; }
        public Assignment(ILineContext context) : base(context)
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res = new XElement(GetType().Name, new XAttribute("Identifier", LHS.Name));
            XMLParser.ParseXMLProperties(this, res, prop);
            res.Add(RHS.ToXML(prop));
            return res;
        }
    }
}
