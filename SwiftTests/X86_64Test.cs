using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swift;
using Swift.InstructionSetGenerators;
using System.IO;
using Swift.Instructions;

namespace SwiftTests
{
    [TestClass]
    public class X86_64Test
    {
        [TestMethod]
        public void TestStringAsParameter()
        {
            MemoryStream stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var inst = new X86_64(writer);
            inst.visit(new StringAsParameter(".LC0", 0));
            string actual = Encoding.Default.GetString(stream.ToArray());
            Assert.AreEqual("leaq\t.LC0(%rip), %rcx", actual);
        }
    }
}
