using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    class Token
    {
        public Global.DataType type;
        public object value;
        public Token(Global.DataType type, Object value)
        {
            this.type = type;
            this.value = value;
        }
    }
}
