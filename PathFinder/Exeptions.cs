using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    [Serializable]
    public class DeprecatedMethodException : Exception
    {
        public DeprecatedMethodException() { }
        public DeprecatedMethodException(string message) : base(message) { }
        public DeprecatedMethodException(string message, Exception inner) : base(message, inner) { }
        protected DeprecatedMethodException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
