using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Return from the subroutine
    /// </summary>
    public class Ret : Instruction
    {
        public Ret()
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
