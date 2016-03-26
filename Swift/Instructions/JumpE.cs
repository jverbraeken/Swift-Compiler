using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Jumps to the given label when values are equal
    /// </summary>
    public class JumpE : Instruction
    {
        public string Name { get; set; }
        public JumpE(string name)
        {
            Name = name;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
