using Swift.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class Module
    {
        public List<Instruction> InterCode { get; set; }
        public Dictionary<string, string> StringTable { get; set; }
        
        public Module(List<Instruction> interCode, Dictionary<string, string> stringTable)
        {
            InterCode = interCode;
            StringTable = stringTable;
        }
    }
}
