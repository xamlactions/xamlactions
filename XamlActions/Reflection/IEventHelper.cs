using System;

namespace XamlActions.Reflection {
    public interface IEventHelper {
        void RegisterEvent(object obj, string eventName, Action<object, object> eventHandler);
        void UnregisterEvent(object obj, string eventName, Action<object, object> eventHandler);
    }
}