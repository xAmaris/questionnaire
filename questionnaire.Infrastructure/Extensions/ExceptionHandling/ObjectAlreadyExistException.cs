using System;
using System.Runtime.Serialization;

namespace questionnaire.Infrastructure.Extensions.ExceptionHandling {
    public class ObjectAlreadyExistException : Exception {
        public ObjectAlreadyExistException () { }
        public ObjectAlreadyExistException (string message) : base (message) { }
        public ObjectAlreadyExistException (string message, Exception inner) : base (message, inner) { }
        protected ObjectAlreadyExistException (SerializationInfo info, StreamingContext context) : base (info, context) { }
    }
}