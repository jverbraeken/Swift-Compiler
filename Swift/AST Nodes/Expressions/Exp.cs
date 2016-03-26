using Swift.AST_Nodes;
using Swift.Phrases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public interface Exp
    {
        void accept(Visitor v);
        ASTType accept(TypeVisitor v);
    }
}