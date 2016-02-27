using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public interface Visitor
    {
        Exp visit(AndExp n);
        Exp visit(BitwiseComplementExp n);
        Exp visit(DivisionExp n);
        Exp visit(ExclamationExp n);
        Exp visit(MinusExp n);
        Exp visit(ModuloExp n);
        Exp visit(MultiplicationExp n);
        Exp visit(OrExp powerExp);
        Exp visit(Identifier identifier);
        Exp visit(PlusExp n);
        Exp visit(PowerExp n);
        Exp visit(IntegerLiteral n);
    }
}
