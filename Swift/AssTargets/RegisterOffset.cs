using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AssTargets
{
    public class RegisterOffset : AssTarget
    {
        public Global.Registers Value { get; set; }
        public int Offset { get; set; }
        public RegisterOffset(Global.Registers value, int offset)
        {
            Value = value;
            Offset = offset;
        }

        public override string accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
