using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace XamlActions.Reflection {
    public class EventHelper {

        public static void RegisterEvent(object obj, string eventName, Action<object, object> eventHandler) {
            EventInfo eventInfo = GetEventInfo(obj, eventName);
            Delegate del = eventHandler.GetMethodInfo().CreateDelegate(eventInfo.EventHandlerType, eventHandler.Target);
            WindowsRuntimeMarshal.AddEventHandler(
                       dlg => (EventRegistrationToken)eventInfo.AddMethod.Invoke(obj, new object[] { dlg }),
                       etr => eventInfo.RemoveMethod.Invoke(obj, new object[] { etr }),
                       del);
        }

        public static void UnregisterEvent(object obj, string eventName, Action<object, object> eventHandler) {
            EventInfo eventInfo = GetEventInfo(obj, eventName);
            Delegate del = eventHandler.GetMethodInfo().CreateDelegate(eventInfo.EventHandlerType, eventHandler.Target);
            WindowsRuntimeMarshal.RemoveEventHandler(
                       dlg => eventInfo.RemoveMethod.Invoke(obj, new object[] { dlg }),
                       del);
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
