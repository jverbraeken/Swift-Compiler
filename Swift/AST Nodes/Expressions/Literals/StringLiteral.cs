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
    public interface IStringLiteral : Exp, ILiteral
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
            XElement res = new XElement(GetType().Name);
            XMLParser.ParseXMLProperties(this, res, prop);
            foreach (IStringElement element in Elements)
            {
                res.Add(element.ToXML(prop));
            }
            return res;
        }
    }

    public interface IStringElement : IASTNode
    {
        Global.ElementTypes ElementType { get; }
        string QuotedTextItem { get; }
        Exp Expression { get; }
        Global.EscapedCharacter EscapedCharacter { get; }
    }

    public class StringElement : ASTNode, IStringElement
    {
        public Global.ElementTypes ElementType { get; internal set; }
        public string QuotedTextItem { get; internal set; }
        public Exp Expression { get; internal set; }
        public Global.EscapedCharacter EscapedCharacter { get; internal set; }

        public StringElement(ILineContext context, string quotedTextItem) : base(context)
        {
            ElementType = Global.ElementTypes.quotedTextItem;
            QuotedTextItem = quotedTextItem;
        }

        public StringElement(ILineContext context, Exp expression) : base(context)
        {
            ElementType = Global.ElementTypes.interpolation;
            Expression = expression;
        }

        public StringElement(ILineContext context, Global.EscapedCharacter escapedCharacter) : base(context)
        {
            ElementType = Global.ElementTypes.escapedCharacter;
            EscapedCharacter = escapedCharacter;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }

        public override XElement ToXML(XMLParser.XMLProperties prop)
        {
            XElement res = new XElement(GetType().Name, new XAttribute("ElementType", Enum.GetName(typeof(Global.ElementTypes), ElementType)));
            switch (ElementType)
            {
                case Global.ElementTypes.escapedCharacter: res.Add(new XAttribute("Value", Enum.GetName(typeof(Global.EscapedCharacter), EscapedCharacter))); break;
                case Global.ElementTypes.interpolation: res.Add(Expression.ToXML(prop)); break;
                case Global.ElementTypes.quotedTextItem: res.Add(new XAttribute("Value", QuotedTextItem)); break;
            }
            return res;
        }
    }


    [Serializable()]
    public class InternalError : SwiftException
    {
        public InternalError() : base() { }
        public InternalError(string message) : base(message) { }
    }
}
