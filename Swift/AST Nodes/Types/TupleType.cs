using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes.Types
{
    public class TupleType : ASTType
    {
        public List<TupleElement> elements { get; set; }
        public TupleType()
        {
            elements = new List<TupleElement>();
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
