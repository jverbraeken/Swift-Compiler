using System;
using Swift.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class ASTFile : ASTNode
    {
        private string filePath;
        public ASTFile(LineContext context, string filePath) : base(context)
        {
            this.filePath = filePath;
        }
    }
}
