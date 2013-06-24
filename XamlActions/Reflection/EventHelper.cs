using System;

namespace XamlActions.Reflection {
    public static class EventHelper {

        public static IEventHelper Default { get; set; }

        static EventHelper() {
            Type type = PlatformResolver.ResolveImplementation<IEventHelper>();
            if(type == null) return;
            Default = (IEventHelper) Activator.CreateInstance(type);
        }
    }
}