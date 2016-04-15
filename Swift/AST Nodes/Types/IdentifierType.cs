using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes.Types
{
    public class IdentifierType : ASTType
    {
        public Identifier Id { get; set; }
        public IdentifierType(Identifier id, bool optional = false) : base(optional)
        {
            Id = id;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
