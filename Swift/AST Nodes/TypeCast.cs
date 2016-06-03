using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift.AST_Nodes
{
    public class TypeCast : ASTNode
    {
        public ASTType Type { get; set; }
        public Exp Target { get; set; }

        public TypeCast(ILineContext context) : base(context)
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res = new XElement(GetType().Name, new XAttribute("Type", Type.GetType().Name));
            XMLParser.ParseXMLProperties(this, res, prop);
            res.Add("Target", Target.ToXML(prop));
            return res;
        }
    }
}
