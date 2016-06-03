using Swift.AST_Nodes;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift.AST_Nodes
{
    public class TupleElement : ASTNode, ITupleParentElement
    {
        public ASTType Type { get; set; }
        public string Name { get; set; }


        public TupleElement(ILineContext context, ASTType type) : base(context)
        {
            Type = type;
            Name = null;
        }

        public TupleElement(ILineContext context, ASTType type, string name) : base(context)
        {
            Type = type;
            Name = name;
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res = new XElement(GetType().Name, new XAttribute("Name", Name), new XAttribute("Type", Type.GetType().Name));
            XMLParser.ParseXMLProperties(this, res, prop);
            return res;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
