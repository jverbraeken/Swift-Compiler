using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AssTargets
{
    public class ByteConstant : AssTarget
    {
        public int Value { get; set; }
        /// <summary>
        /// Stores an integer that will be multiplied by a certain factor, eg 8 for 64-bit systems
        /// </summary>
        /// <param name="value"></param>
        public ByteConstant(int value)
        {
            Value = value;
        }

        public string accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
