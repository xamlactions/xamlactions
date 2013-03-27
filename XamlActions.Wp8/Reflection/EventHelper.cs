using System;
using System.Reflection;

namespace XamlActions.Reflection {
    public class EventHelper {
        public static void RegisterEvent(object obj, string eventName, Action<object, object> eventHandler) {
            EventInfo eventInfo = GetEventInfo(obj, eventName);
            Delegate del = Delegate.CreateDelegate(eventInfo.EventHandlerType, eventHandler.Target, eventHandler.Method);
            eventInfo.AddEventHandler(obj, del);
        }

        public static void UnregisterEvent(object obj, string eventName, Action<object, object> eventHandler) {
            EventInfo eventInfo = GetEventInfo(obj, eventName);
            Delegate del = Delegate.CreateDelegate(eventInfo.EventHandlerType, eventHandler.Target, eventHandler.Method);
            eventInfo.RemoveEventHandler(obj, del);
        }

        public static void RaiseEvent(object obj, string eventName, params object[] eventArgs) {
            var eventDelagate =
                (MulticastDelegate) Reflector.Get(obj, eventName);

            Delegate[] delegates = eventDelagate.GetInvocationList();

            foreach (Delegate dlg in delegates) {
                dlg.GetMethodInfo().Invoke(dlg.Target, eventArgs);
            }
        }

        private static EventInfo GetEventInfo(object obj, string eventName) {
            Type type = obj.GetType();
            return type.GetRuntimeEvent(eventName);
        }
    }
}