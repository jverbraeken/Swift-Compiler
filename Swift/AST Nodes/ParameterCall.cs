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
    public class ParameterCall : ASTNode
    {
        public Exp Value { get; set; }
        /// <summary>
        /// The external name of the ParameterDeclaration
        /// </summary>
        public string Name { get; set; }

        public ParameterCall(ILineContext context, Exp value) : base(context)
        {
            Value = value;
        }

        public ParameterCall(ILineContext context, Exp value, string name) : base(context)
        {
            Value = value;
            Name = name;
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res;
            if (Name == null)
                res = new XElement(GetType().Name);
            else
                res = new XElement(GetType().Name, new XAttribute("Name", Name));
            XMLParser.ParseXMLProperties(this, res, prop);
            res.Add(Value.ToXML(prop));
            return res;
        }

        public override void accept(Visitor v)
        {
            throw new NotImplementedException();
        }
    }
}
