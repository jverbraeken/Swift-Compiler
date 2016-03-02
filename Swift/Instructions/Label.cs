using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Defines a new label
    /// </summary>
    public class Label : Instruction
    {
        private string name;
        public Label(string name)
        {
            this.name = name;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
