using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Bitwise And
    /// </summary>
    public class And : Instruction
    {
        public AssTarget Val1 { get; set; }
        public AssTarget Val2 { get; set; }
        public And(AssTarget val1, AssTarget val2)
        {
            Val1 = val1;
            Val2 = val2;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
