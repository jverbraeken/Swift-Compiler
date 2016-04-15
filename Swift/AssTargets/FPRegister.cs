using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AssTargets
{
    /// <summary>
    /// Floating Point Register
    /// </summary>
    public class FPRegister : AssTarget
    {
        /// <summary>
        /// Zero-based; indicates which floating point register we want to use
        /// </summary>
        public int Position { get; set; }
        public FPRegister(int position)
        {
            Position = position;
        }

        public string accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
