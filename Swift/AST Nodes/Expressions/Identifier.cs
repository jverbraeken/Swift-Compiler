using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class Identifier
    {
        public string Name { get; set; }
        public Identifier(string name)
        {
            Name = name;
        }
        public void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
