using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AssTargets
{
    public class BinaryConstant : AssTarget
    {
        public int Value { get; set; }
        public BinaryConstant(int value)
        {
            Value = value;
        }

        public string accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
