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
    public class ConstDeclaration : ASTNode
    {
        public ASTType Type { get; set; }
        public Identifier Name { get; set; }
        public Exp RHS { get; set; }

        public ConstDeclaration(ILineContext context, Exp RHS) : base(context)
        {
            this.RHS = RHS;
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
            XElement res = new XElement(GetType().Name, new XAttribute("Identifier", Name.Name), new XAttribute("Type", Type.GetType().Name));
            XMLParser.ParseXMLProperties(this, res, prop);
            res.Add(RHS.ToXML(prop));
            return res;
        }
    }
}
