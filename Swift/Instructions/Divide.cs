using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Divide Register to Target
    /// </summary>
    public class Divide : Instruction
    {
        public AssTarget Value { get; set; }
        public AssTarget Target { get; set; }
        public Divide(AssTarget value, AssTarget target)
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
