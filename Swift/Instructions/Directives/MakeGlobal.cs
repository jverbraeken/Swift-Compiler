using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions.Directives
{
    /// <summary>
    /// Make the function named method visible to the outer world
    /// </summary>
    public class MakeGlobal : Instruction
    {
        public string Method { get; set; }
        public MakeGlobal(string method)
        {
            Method = method;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
