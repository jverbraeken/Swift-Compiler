using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class IdentifierExp : Exp
    {
        private Identifier id;
        public IdentifierExp(Identifier id)
        {
            this.id = id;
        }

        public Type accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
