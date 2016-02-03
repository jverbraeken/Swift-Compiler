using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift.Tokens
{
    public class LineContext
    {
        private int pos; //position in the line
        private int line; //line in the source code

        public LineContext(int pos, int line)
        {
            this.pos = pos;
            this.line = line;
        }
    }
}
