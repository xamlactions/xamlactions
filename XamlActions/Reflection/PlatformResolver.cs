using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace XamlActions.Reflection {
    public static class PlatformResolver {
        private static string[] _platforms = new[] {"WinRT", "Wp8"};
        private static Assembly _assemblyTarget;

        public static Type ResolveImplementation(Type interfaceType) {
            EnsureAssemblyTarget();
            return _assemblyTarget.DefinedTypes.FirstOrDefault(x => x.ImplementedInterfaces.Contains(interfaceType)).AsType();
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
            throw new NotImplementedException("Platform not supported. Available options are: " + String.Concat(_platforms, ","));
        }

        private static Assembly LoadAssemblyOrNull(string assemblyName) {
            try {
                var name = new AssemblyName("XamlActions." + assemblyName);
                return Assembly.Load(name);
            }
            catch {
                return null;
            }
        }
    }
}