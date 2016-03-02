using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AssTargets
{
    public class Register : AssTarget
    {
        public Global.Registers Value { get; set; }
        public Register(Global.Registers value)
        {
            Value = value;
        }
    }
}
