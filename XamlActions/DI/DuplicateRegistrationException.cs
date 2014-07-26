using System;

namespace XamlActions.DI {
    public class DuplicateRegistrationException : Exception {
        public Type AlreadyRegisteredType { get; set; }

        public DuplicateRegistrationException(Type typeKey) : this(typeKey, null, null) { }
        public DuplicateRegistrationException(Type typeKey, string message) : this(typeKey, message, null) { }

        public DuplicateRegistrationException(Type typeKey, string message = "Type already registered", Exception inner = null) : base(message, inner) {
            AlreadyRegisteredType = typeKey;
        }
    }
}