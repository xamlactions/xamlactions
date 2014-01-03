using System;
using System.Reflection;

namespace XamlActions {
    public class WeakDelegate : WeakReference {
        private MethodInfo _method;
        private WeakReference _actionTarget;

        public WeakDelegate(object actionOwner, Delegate action)
            : base(actionOwner) {
            _method = action.Method;
            _actionTarget = new WeakReference(action.Target);
        }

        public void Execute(object message) {
            if (!base.IsAlive) {
                return;
            }
            _method.Invoke(_actionTarget.Target, new[] {message});
        }
    }
}