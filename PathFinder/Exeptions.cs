using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{

    [Serializable]
    public class MissingInterfaceException : Exception
    {
        public MissingInterfaceException() { }
        public MissingInterfaceException(string message) : base(message) { }
        public MissingInterfaceException(string message, Exception inner) : base(message, inner) { }
        protected MissingInterfaceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
    [Serializable]
    public class InvalidCallException : Exception
    {
        public InvalidCallException() { }
        public InvalidCallException(string message) : base(message) { }
        public InvalidCallException(string message, Exception inner) : base(message, inner) { }
        protected InvalidCallException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
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

    [Serializable]
    public class InvalidConstructorCallException : Exception
    {
        public InvalidConstructorCallException() { }
        public InvalidConstructorCallException(string message) : base(message) { }
        public InvalidConstructorCallException(string message, Exception inner) : base(message, inner) { }
        protected InvalidConstructorCallException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
