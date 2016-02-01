using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Expressions
{
    class ExclamationExp : Exp
    {
        public Exp e1, e2;
        public ExclamationExp(Exp e1, Exp e2)
        {
            this.e1 = e1;
            this.e2 = e2;
        }
        public int accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
