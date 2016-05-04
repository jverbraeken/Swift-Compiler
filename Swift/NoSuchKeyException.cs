using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swift
{
    [Serializable()]
    public class NoSuchKeyException : System.Exception
    {
        public NoSuchKeyException() : base() { }
        public NoSuchKeyException(string message) : base(message) { }
        public NoSuchKeyException(string message, Exception inner) : base(message, inner) { }
        protected NoSuchKeyException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }
}
