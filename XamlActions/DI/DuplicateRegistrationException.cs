using System;

namespace XamlActions.DI {
    public class DuplicateRegistrationException : Exception {
        public DuplicateRegistrationException() { }
        public DuplicateRegistrationException(string message) : base(message) { }
        public DuplicateRegistrationException(string message, Exception inner) : base(message, inner) { }
    }
}