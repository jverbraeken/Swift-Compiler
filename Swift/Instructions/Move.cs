using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Move the value to To
    /// </summary>
    public class Move : Instruction
    {
        public AssTarget From { get; set; }
        public AssTarget To { get; set; }
        public Move(AssTarget from, AssTarget to)
        {
            From = from;
            To = to;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
