using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Instructions
{
    /// <summary>
    /// Call the function labeled with Name
    /// </summary>
    public class Call : Instruction
    {
        public string Name { get; set; }
        public Call(string name)
        {
            Name = name;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
