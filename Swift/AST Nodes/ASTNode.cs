using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class ASTNode
    {
        private Global.ASTType type;
        private List<ASTNode> children; //The arguments for a function call
        private LineContext context;
        private string name;
        private ASTNode returnType;
        private Table scope;

        public ASTNode(Global.ASTType type, LineContext context)
        {
            children = new List<ASTNode>();
            this.type = type;
            this.context = context;
        }

        public void AddNode(ASTNode node)
        {
            children.Add(node);
        }

        public void SetChildren(List<ASTNode> children)
        {
            this.children = children;
        }

        public List<ASTNode> GetChildren()
        {
            return children;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public new Global.ASTType GetType()
        {
            return type;
        }

        public void SetReturnType(ASTNode returnType)
        {
            this.returnType = returnType;
        }

        public ASTNode GetReturnType()
        {
            return returnType;
        }

        public void SetScope(Table scope)
        {
            this.scope = scope;
        }

        public Table GetScope()
        {
            return scope;
        }

        public LineContext GetContext()
        {
            return context;
        }
    }
}
