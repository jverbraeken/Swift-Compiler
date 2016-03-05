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
        public AssTarget Target { get; set; }
        public Push(AssTarget target)
        {
            Target = target;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
