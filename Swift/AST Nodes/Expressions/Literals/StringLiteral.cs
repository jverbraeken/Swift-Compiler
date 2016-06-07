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
    public interface IStringLiteral : Exp
    {
        List<IStringElement> Elements { get; set; }
    }

    public class StringLiteral : Literal, Exp, IStringLiteral
    {
        public List<IStringElement> Elements { get; set; }

        public StringLiteral(ILineContext context) : base(context)
        {
            Elements = new List<IStringElement>();
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
            XElement res = new XElement(GetType().Name, new XAttribute("Value", Value));
            XMLParser.ParseXMLProperties(this, res, prop);
            return res;
        }
    }

    public interface IStringElement
    {
        Global.ElementTypes ElementType { get; }
        string QuotedTextItem { get; }
        Exp Expression { get; }
        Global.EscapedCharacter EscapedCharacter { get; }
    }

    public class StringElement : IStringElement
    {
        public Global.ElementTypes ElementType { get; internal set; }
        public string QuotedTextItem { get; internal set; }
        public Exp Expression { get; internal set; }
        public Global.EscapedCharacter EscapedCharacter { get; internal set; }

        public StringElement(string quotedTextItem)
        {
            ElementType = Global.ElementTypes.quotedTextItem;
            QuotedTextItem = quotedTextItem;
        }

        public StringElement(Exp expression)
        {
            ElementType = Global.ElementTypes.interpolation;
            Expression = expression;
        }

        public StringElement(Global.EscapedCharacter escapedCharacter)
        {
            ElementType = Global.ElementTypes.escapedCharacter;
            EscapedCharacter = escapedCharacter;
        }
    }


    [Serializable()]
    public class InternalError : SwiftException
    {
        public InternalError() : base() { }
        public InternalError(string message) : base(message) { }
    }
}
