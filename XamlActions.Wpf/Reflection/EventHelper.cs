using System;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;

namespace XamlActions.Reflection {
    public class EventHelperWpf : IEventHelper {
        public static IEventHelper Default = new EventHelperWpf();

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

        private EventInfo GetEventInfo(object obj, string eventName) {
            Type type = obj.GetType();
            return type.GetRuntimeEvent(eventName);
        }
    }
}