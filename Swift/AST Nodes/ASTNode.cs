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
        private Global.ASTType type; //The type of the node, eg a function, a string, a class
        private List<ASTNode> children; //The arguments for a function call
        private LineContext context; //For debugging, gives the programmer an idea where the node is located in the original source code
        private string name; //The name of the function, the string, ...
        private ASTNode returnType; //The return type of a function
        private Table scope; //The scope every variable, function, etc. is made in
        private int assemblyLocation; //The location the node is located when compiled to assembly (mostly used for constant variables)

        /// <summary>
        /// Should not be called directly, but only by its children
        /// </summary>
        /// <param name="type"></param>
        /// <param name="context"></param>
        public ASTNode(LineContext context)
        {
            this.context = context;
        }

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

        public void SetAssemblyLocation(int assemblyLocation)
        {
            this.assemblyLocation = assemblyLocation;
        }

        public int GetAssemblyLocation()
        {
            return assemblyLocation;
        }

        public LineContext GetContext()
        {
            return context;
        }
    }
}
