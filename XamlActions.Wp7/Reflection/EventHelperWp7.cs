using System;
using System.Reflection;

namespace XamlActions.Reflection {
    public class EventHelperWp7 : IEventHelper {

        public static IEventHelper Default = new EventHelperWp7();

        public void RegisterEvent(object obj, string eventName, Action<object, object> eventHandler) {
            EventInfo eventInfo = GetEventInfo(obj, eventName);
            Delegate del = Delegate.CreateDelegate(eventInfo.EventHandlerType, eventHandler.Target, eventHandler.Method);
            eventInfo.AddEventHandler(obj, del);
        }

        public void UnregisterEvent(object obj, string eventName, Action<object, object> eventHandler) {
            EventInfo eventInfo = GetEventInfo(obj, eventName);
            Delegate del = Delegate.CreateDelegate(eventInfo.EventHandlerType, eventHandler.Target, eventHandler.Method);
            eventInfo.RemoveEventHandler(obj, del);
        }

        private static EventInfo GetEventInfo(object obj, string eventName) {
            Type type = obj.GetType();
            return type.GetEvent(eventName);
        }
    }
}