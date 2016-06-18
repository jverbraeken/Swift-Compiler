using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Swift;
using Swift.AST_Nodes;
using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwiftTests
{
    [TestClass]
    public class ToXMLTest
    {
        [TestMethod]
        public void TestBase()
        {
            var context0 = Substitute.For<ILineContext>();
            context0.ToXML().Returns(new XElement("LineContext", new XAttribute[] { new XAttribute("pos", 0), new XAttribute("line", 0) }));
            var context1 = Substitute.For<ILineContext>();
            context1.ToXML().Returns(new XElement("LineContext", new XAttribute[] { new XAttribute("pos", 1), new XAttribute("line", 2)}));
            var context2 = Substitute.For<ILineContext>();
            context2.ToXML().Returns(new XElement("LineContext", new XAttribute[] { new XAttribute("pos", 3), new XAttribute("line", 4) }));
            Exp exp1 = Substitute.For<Exp>();
            Exp exp2 = Substitute.For<Exp>();
            Base node = new Base(context0);
            FunctionCallExp func = new FunctionCallExp(context0);
            func.Name = new Identifier("funcName");
            ParameterCall param1 = new ParameterCall(context1, exp1);
            param1.Name = "param1";
            ParameterCall param2 = new ParameterCall(context2, exp2);
            param2.Name = "param2";
            func.Args.Add(param1);
            func.Args.Add(param2);
            node.Children.Add(func);
            Assert.AreEqual(
@"<Base>
  <FunctionCallExp Name=""funcName"">
    <ParameterCall Name=""param1"" />
    <ParameterCall Name=""param2"" />
  </FunctionCallExp>
</Base>", node.ToXML(new XMLParser.XMLProperties(false, false)).ToString());
        }
    }
}