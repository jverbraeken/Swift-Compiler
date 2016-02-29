using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class Identifier : ASTNode, Exp
    {
        public string Name { get; set; }
        public Identifier(LineContext context, string name) : base(context)
        {
            Name = name;
        }
        public Exp accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
