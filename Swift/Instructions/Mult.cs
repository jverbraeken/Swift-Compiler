using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Multiply Register to Target
    /// </summary>
    public class Mult : Instruction
    {
        public AssTarget Value { get; set; }
        public AssTarget Target { get; set; }
        public Mult(AssTarget value, AssTarget target)
        {
            Value = value;
            Target = target;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
