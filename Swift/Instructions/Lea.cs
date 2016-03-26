using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Load Effective Addres
    /// </summary>
    public class Lea : Instruction
    {
        public AssTarget From { get; set; }
        public AssTarget To { get; set; }
        public Lea(AssTarget from, AssTarget to)
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
