using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    public class Type
    {
        public static PrimitiveType INT = new PrimitiveType(Global.DataType.INT);
        public static PrimitiveType UINT = new PrimitiveType(Global.DataType.UINT);
        public static PrimitiveType STRING = new PrimitiveType(Global.DataType.STRING);
        public static PrimitiveType BOOL = new PrimitiveType(Global.DataType.BOOL);
        public static PrimitiveType FLOAT = new PrimitiveType(Global.DataType.FLOAT);
        public static PrimitiveType DOUBLE = new PrimitiveType(Global.DataType.DOUBLE);
        public static PrimitiveType CHARACTER = new PrimitiveType(Global.DataType.CHARACTER);
        public static PrimitiveType OINT = new PrimitiveType(Global.DataType.INT);
        public static PrimitiveType OUINT = new PrimitiveType(Global.DataType.OUINT);
        public static PrimitiveType OSTRING = new PrimitiveType(Global.DataType.OSTRING);
        public static PrimitiveType OBOOL = new PrimitiveType(Global.DataType.OBOOL);
        public static PrimitiveType OFLOAT = new PrimitiveType(Global.DataType.OFLOAT);
        public static PrimitiveType ODOUBLE = new PrimitiveType(Global.DataType.ODOUBLE);
        public static PrimitiveType OCHARACTER = new PrimitiveType(Global.DataType.OCHARACTER);
        public static PrimitiveType VOID = new PrimitiveType(Global.DataType.VOID);
    }

    public class PrimitiveType : Type
    {
        private Global.DataType type;

        public PrimitiveType(Global.DataType type)
        {
            this.type = type;
        }
    }
}
