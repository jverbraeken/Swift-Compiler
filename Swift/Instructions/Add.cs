using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Adds RDX to RAX and stores the result in RAX
    /// </summary>
    public class Add : Instruction
    {
        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
