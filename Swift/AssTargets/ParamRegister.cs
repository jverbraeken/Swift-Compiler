using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AssTargets
{
    /// <summary>
    /// A register that holds the value of a parameter
    /// </summary>
    public class ParamRegister : AssTarget
    {
        /// <summary>
        /// Zero-based; indicates which argument is is (eg 2 means third argument register)
        /// </summary>
        public int Position { get; set; }
        public ParamRegister(int position)
        {
            Position = position;
        }

        public string accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
