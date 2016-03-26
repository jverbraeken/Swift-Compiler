using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Jumps to the given label when result is greater or equal
    /// </summary>
    public class JumpGE : Instruction
    {
        public string Name { get; set; }
        public JumpGE(string name)
        {
            Name = name;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
