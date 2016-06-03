using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swift.Phrases;
using System.Xml.Linq;

namespace Swift.AST_Nodes
{
    public class FunctionCallExp : ASTNode, Exp
    {
        public Identifier Name { get; set; }
        public List<ParameterCall> Args { get; set; }
        public FunctionCallExp(ILineContext context) : base(context)
        {
            Args = new List<ParameterCall>();
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
            XElement res = new XElement(GetType().Name, new XAttribute("Name", Name.Name));
            XMLParser.ParseXMLProperties(this, res, prop);
            foreach (ParameterCall param in Args)
            {
                res.Add(param.ToXML(prop));
            }
            return res;
        }
    }
}
