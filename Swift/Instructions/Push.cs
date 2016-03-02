using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Defines a push to the stack
    /// </summary>
    public class Push : Instruction
    {
        private AssTarget target;
        public Push(AssTarget target)
        {
            this.target = target;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
