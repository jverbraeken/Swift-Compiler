using Swift.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class ASTString : ASTNode
    {
        private string value;
        public ASTString(string value, LineContext context) : base(context)
        {
            this.value = value;
        }

        public string GetValue()
        {
            return value;
        }
    }
}
