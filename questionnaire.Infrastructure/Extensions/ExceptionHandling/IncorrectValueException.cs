using System;
using System.Runtime.Serialization;

namespace questionnaire.Infrastructure.Extensions.ExceptionHandling {
    public class IncorrectValueException : Exception {
        public IncorrectValueException () { }
        public IncorrectValueException (string message) : base (message) { }
        public IncorrectValueException (string message, Exception inner) : base (message, inner) { }
        protected IncorrectValueException (SerializationInfo info, StreamingContext context) : base (info, context) { }
    }
}