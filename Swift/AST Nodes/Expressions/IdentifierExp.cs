using Swift.AST_Nodes;
using Swift.Phrases;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift
{
    public class IdentifierExp : ASTNode, Exp
    {
        public Identifier ID
        {
            get; set;
        }
        public IdentifierExp(ILineContext context, Identifier id) : base(context)
        {
            ID = id;
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
            XElement res = new XElement(GetType().Name, new XAttribute("ID", ID.Name));
            XMLParser.ParseXMLProperties(this, res, prop);
            return res;
        }
    }
}
