using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class ASTFunctionCall : ASTNode
    {
        private string name;
        private List<ASTNode> args;
        public ASTFunctionCall(string name, List<ASTNode> args, LineContext context) : base(context)
        {
            this.name = name;
            this.args = args;
        }

        public string GetName()
        {
            return name;
        }

        public List<ASTNode> GetArgs()
        {
            return args;
        }
    }
}
