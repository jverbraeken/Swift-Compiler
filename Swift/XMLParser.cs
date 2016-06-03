using Swift.AST_Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Swift
{
    public static class XMLParser
    {
        public struct XMLProperties
        {
            public bool parseLineContext, parseScope;

            public XMLProperties(bool parseLineContext, bool parseScope)
            {
                this.parseLineContext = parseLineContext;
                this.parseScope = parseScope;
            }
        }

        public static void ParseXMLProperties(ASTNode node, XElement xml, XMLProperties prop)
        {
            if (prop.parseLineContext)
                xml.Add(node.Context.ToXML());
            if (prop.parseScope)
                xml.Add(node.Scope.ToXML());
        }
    }
}
