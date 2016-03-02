using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Defines a pop from the stack
    /// </summary>
    public class Pop : Instruction
    {
        private AssTarget target;
        public Pop(AssTarget target)
        {
            this.target = target;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
