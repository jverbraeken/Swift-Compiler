using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AssTargets
{
    public class HexadecimalConstant : AssTarget
    {
        public int Value { get; set; }
        public HexadecimalConstant(int value)
        {
            Value = value;
        }

        public string accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
