using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    public abstract class Instruction
    {
        public abstract void accept(Visitor v);
    }
}
