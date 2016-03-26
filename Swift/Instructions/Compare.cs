using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Compares Val1 and Val2
    /// </summary>
    public class Compare : Instruction
    {
        public AssTarget Val1 { get; set; }
        public AssTarget Val2 { get; set; }
        public Compare(AssTarget val1, AssTarget val2)
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
