using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    public class StringAsParameter : Instruction
    {
        /// <summary>
        /// The reference to the string (eg .LC0)
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 0 -> it's the first parameter of the function
        /// 1 -> it's the second parameter of the function
        /// etc.
        /// </summary>
        public int Number { get; set; }
        public StringAsParameter(string name, int number)
        {
            Name = name;
            Number = number;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
