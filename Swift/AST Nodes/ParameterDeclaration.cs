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
    public class ParameterDeclaration : ASTNode
    {
        public ASTType Type { get; set; }
        public string InternalName { get; set; }
        public string ExternalName { get; set; }
        public Exp DefaultValue { get; set; }
        public bool InOut { get; set; }
        /// <summary>
        /// By default all Swift parameters are constants
        /// </summary>
        public bool NoConstant { get; set; }

        public ParameterDeclaration(ILineContext context, ASTType type, string name) : base(context)
        {
            Type = type;
            InternalName = name;
            ExternalName = null;
            DefaultValue = null;
            InOut = false;
            NoConstant = false;
        }

        public ParameterDeclaration(ILineContext context, ASTType type, string name, bool sameInternalExternalName) : base(context)
        {
            Type = type;
            InternalName = name;
            if (sameInternalExternalName)
                ExternalName = name;
            else
                ExternalName = null;
            DefaultValue = null;
            InOut = false;
            NoConstant = false;
        }

        public ParameterDeclaration(ILineContext context, ASTType type, string internalName, string externalName) : base(context)
        {
            Type = type;
            InternalName = internalName;
            ExternalName = externalName;
            DefaultValue = null;
            InOut = false;
            NoConstant = false;
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res = new XElement(GetType().Name, new XAttribute("Type", Type.GetType().Name), new XAttribute("InternalName", InternalName), new XAttribute("ExternalName", ExternalName), new XAttribute("InOut", InOut), new XAttribute("NoConstant", NoConstant));
            XMLParser.ParseXMLProperties(this, res, prop);
            res.Add("DefaultValue", DefaultValue.ToXML(prop));
            return res;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
