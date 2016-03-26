using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AssTargets
{
    public class RegisterOffset : AssTarget
    {
        public Global.Registers Register { get; set; }
        /// <summary>
        /// If IntOffset equals null, then the offset is defined by a label and vice versa
        /// </summary>
        public int? IntOffset { get; set; }
        public string LabelOffset { get; set; }

        public RegisterOffset(Global.Registers value, int offset)
        {
            Register = value;
            IntOffset = offset;
            LabelOffset = null;
        }

        public RegisterOffset(Global.Registers value, string offset)
        {
            Register = value;
            IntOffset = null;
            LabelOffset = offset;
        }

        public string accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
