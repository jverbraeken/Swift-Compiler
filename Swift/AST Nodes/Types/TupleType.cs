using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes.Types
{
    public class TupleType : ASTType
    {
        public TupleElementList elements { get; set; }
        public TupleType(bool optional = false) : base(optional)
        {
            elements = new TupleElementList(null);
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
