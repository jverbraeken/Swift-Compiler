using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AssTargets
{
    public class Constant : AssTarget
    {
        public int Value { get; set; }
        public Constant(int value)
        {
            Value = value;
        }
    }
}
