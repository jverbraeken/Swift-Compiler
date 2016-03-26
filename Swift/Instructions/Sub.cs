using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Subtract Register from Target
    /// </summary>
    public class Sub : Instruction
    {
        public AssTarget Value { get; set; }
        public AssTarget Target { get; set; }
        public Sub(AssTarget value, AssTarget target)
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
