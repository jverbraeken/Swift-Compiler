using Swift.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    interface Visitor
    {
        int visit(AndExp n);
        int visit(BitwiseComplementExp n);
        int visit(DivisionExp n);
        int visit(ExclamationExp n);
        int visit(MinusExp n);
        int visit(ModuloExp n);
        int visit(MultiplicationExp n);
        int visit(OrExp powerExp);
        int visit(PlusExp n);
        int visit(PowerExp n);
    }
}
