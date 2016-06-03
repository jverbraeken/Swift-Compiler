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
    public class TupleElementList : ASTNode, ITupleParentElement
    {

        public List<ITupleParentElement> List;

        public TupleElementList(ILineContext context) : base(context)
        {
            List = new List<ITupleParentElement>();
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res = new XElement(GetType().Name);
            XMLParser.ParseXMLProperties(this, res, prop);
            foreach (ITupleParentElement element in List)
                res.Add(element.ToXML(prop));
            return res;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}