using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using CASTOR2.Core.Base.Interfaces;

namespace CASTOR2.Core.Base.Exceptions
{
    [Serializable()]
    public class PrecedenceException : Exception
    {
        public PrecedenceException() : base() { }
        public PrecedenceException(string message) : base(message) { }
        public PrecedenceException(string message, Exception innerException) : base(message, innerException) { }
        protected PrecedenceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
