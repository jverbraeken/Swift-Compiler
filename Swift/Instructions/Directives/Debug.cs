using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions.Directives
{
    public class Debug : Instruction
    {
        private string info;
        public Debug(string info)
        {
            this.info = info;
        }

        public override string accept(Visitor v)
        {
            return v.visit(this);
        }

        public override string ToString()
        {
            return info;
        }
    }
}
