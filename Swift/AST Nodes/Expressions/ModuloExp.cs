﻿using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class ModuloExp : ASTNode, Exp
    {
        public Exp e1, e2;
        public ModuloExp(LineContext context, Exp e1, Exp e2) : base(context)
        {
            this.e1 = e1;
            this.e2 = e2;
        }
        public Exp accept(Visitor v)
        {
            return v.visit(this);
        }
    }
}
