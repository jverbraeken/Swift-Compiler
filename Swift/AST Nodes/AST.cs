using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class AST : ASTNode
    {
        private List<ASTNode> children;
        public AST(LineContext context) : base(context)
        {
            children = new List<ASTNode>();
        }

        public void AddNode(ASTNode node)
        {
            children.Add(node);
        }
    }
}