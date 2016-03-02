using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Defines a new comment
    /// </summary>
    public class Comment : Instruction
    {
        private string name;
        public Comment(string name)
        {
            this.name = name;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
