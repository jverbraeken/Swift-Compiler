using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions.Directives
{
    public class SectionCode : Instruction
    {
        public override string accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
