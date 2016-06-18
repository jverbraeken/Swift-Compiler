using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift.AST_Nodes
{
    public class Base : ASTNode
    {
        public List<ASTNode> Children { get; set; }

        public Base(ILineContext context) : base(context)
        {
            Children = new List<ASTNode>();
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res = new XElement(GetType().Name);
            XMLParser.ParseXMLProperties(this, res, prop);
            foreach (ASTNode child in Children)
            {
                var @switch = new Dictionary<Type, Action>
                {
                    { typeof(FunctionCallExp), () => res.Add(((FunctionCallExp) child).ToXML(prop)) }
                };
                @switch[child.GetType()]();
            }
            return res;
        }
    }
}
