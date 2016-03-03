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
        public string Method { get; set; }
        public Label(string method)
        {
            Method = method;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
