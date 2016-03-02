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
    public class Nope : Instruction
    {
        public Nope()
        {
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
