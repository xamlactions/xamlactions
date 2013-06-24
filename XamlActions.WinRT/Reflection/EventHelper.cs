using System;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;

namespace XamlActions.Reflection {
    public class EventHelperWinRT : IEventHelper {
        public static IEventHelper Default = new EventHelperWinRT();

        public void RegisterEvent(object obj, string eventName, Action<object, object> eventHandler) {
            EventInfo eventInfo = GetEventInfo(obj, eventName);
            Delegate del = eventHandler.GetMethodInfo().CreateDelegate(eventInfo.EventHandlerType, eventHandler.Target);
            WindowsRuntimeMarshal.AddEventHandler(
                dlg => (EventRegistrationToken) eventInfo.AddMethod.Invoke(obj, new object[] {dlg}),
                etr => eventInfo.RemoveMethod.Invoke(obj, new object[] {etr}),
                del);
        }

        public void UnregisterEvent(object obj, string eventName, Action<object, object> eventHandler) {
            EventInfo eventInfo = GetEventInfo(obj, eventName);
            Delegate del = eventHandler.GetMethodInfo().CreateDelegate(eventInfo.EventHandlerType, eventHandler.Target);
            WindowsRuntimeMarshal.RemoveEventHandler(
                dlg => eventInfo.RemoveMethod.Invoke(obj, new object[] {dlg}),
                del);
        }

        private EventInfo GetEventInfo(object obj, string eventName) {
            Type type = obj.GetType();
            return type.GetRuntimeEvent(eventName);
        }
    }
}