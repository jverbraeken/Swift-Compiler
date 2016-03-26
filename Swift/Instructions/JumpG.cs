using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Jumps to the given label when result is greater
    /// </summary>
    public class JumpG : Instruction
    {
        public string Name { get; set; }
        public JumpG(string name)
        {
            Name = name;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
