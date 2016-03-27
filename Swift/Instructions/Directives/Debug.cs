using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions.Directives
{
    public class Debug : Instruction
    {
        public string Info
        {
            get; set;
        }
        public Debug(string info)
        {
            Info = info;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
