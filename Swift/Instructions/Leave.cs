using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Do nothing
    /// </summary>
    public class Leave : Instruction
    {
        public Leave()
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
