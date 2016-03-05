using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.AST_Nodes.Types
{
    public class IdentifierType : ASTType
    {
        private Identifier id;
        public IdentifierType(Identifier id)
        {
            this.id = id;
        }

        public override void accept(Visitor v)
        {
            v.visit(this);
        }
    }
}
