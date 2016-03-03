using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public interface AssTarget
    {
        string accept(Visitor v);
    }
}
