using System;
using System.Runtime.Serialization;

namespace questionnaire.Infrastructure.Extensions.ExceptionHandling {
    public class InccorectRoleException : Exception {

        public InccorectRoleException () { }
        public InccorectRoleException (string message) : base (message) { }
        public InccorectRoleException (string message, Exception inner) : base (message, inner) { }
        protected InccorectRoleException (SerializationInfo info, StreamingContext context) : base (info, context) { }

    }
}