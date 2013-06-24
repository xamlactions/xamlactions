using System;
using XamlActions.Reflection;

namespace XamlActions.ViewServices {
    public static class DispatcherHelper {
        public static IDispatcher Default { get; set; }

        static DispatcherHelper() {
            Type type = PlatformResolver.ResolveImplementation<IDispatcher>();
            if (type == null) return;
            Default = (IDispatcher)Activator.CreateInstance(type);
        }
    }
}