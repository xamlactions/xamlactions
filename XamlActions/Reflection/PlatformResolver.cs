using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace XamlActions.Reflection {
    public static class PlatformResolver {
        private static string[] _platforms = { "WinRT", "Wp8", "Universal", "Wpf" };
        private static Assembly _assemblyTarget;

        public static Type ResolveImplementation(Type interfaceType) {
            EnsureAssemblyTarget();
            return _assemblyTarget.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(interfaceType));
            //return _assemblyTarget.DefinedTypes.FirstOrDefault(x => x.ImplementedInterfaces.Contains(interfaceType)).AsType();
        }

        public static Type ResolveImplementation<T>() {
            return ResolveImplementation(typeof (T));
        }

        private static void EnsureAssemblyTarget() {
            if (_assemblyTarget != null) return;
            _assemblyTarget = LoadAssemblyTarget();
        }

        private static Assembly LoadAssemblyTarget() {
            for (int i = 0; i < _platforms.Length; i++) {
                Assembly assembly = LoadAssemblyOrNull(_platforms[i]);
                if (assembly != null) return assembly;
            }
            Debug.WriteLine("Warning: Platform not supported. Available options are: " + String.Concat(_platforms, ","));
            return null;
        }

        private static Assembly LoadAssemblyOrNull(string assemblyName) {
            try {
                return Assembly.Load("XamlActions." + assemblyName);
            }
            catch {
                return null;
            }
        }
    }
}