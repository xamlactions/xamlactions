using System;
using System.Collections.Generic;
using System.Linq;
using XamlActions.DI;
using XamlActions.Reflection;
using XamlActions.ViewServices;

namespace XamlActions {
    public abstract class ViewModelLocatorBase {
        private IDesignModeChecker _designModeChecker;

        protected ViewModelLocatorBase() {
            DefaultRegistration.EnsureRegistered();
            _designModeChecker = Resolve<IDesignModeChecker>();

            CallFirst();
            if (_designModeChecker.IsInDesignMode()) {
                CallOnlyInDesigntime();
            }
            else {
                CallOnlyInRuntime();
            }
            CallLast();
        }

        public abstract void CallFirst();
        public abstract void CallOnlyInRuntime();
        public abstract void CallOnlyInDesigntime();
        public abstract void CallLast();

        public void ResolveAllPropertiesWithViewModelAsPrefix() {
            IEnumerable<string> names = Reflector.ListAllProperties(GetType()).Where(x => x.EndsWith("ViewModel"));
            foreach (string name in names) {
                Type vmType = Reflector.GetPropertyType(GetType(), name);
                Reflector.SetProperty(this, name, ServiceLocator.Default.Resolve(vmType));
            }
        }

        public T Resolve<T>() where T : class {
            return ServiceLocator.Default.Resolve<T>();
        }

        public object Resolve(Type type) {
            return ServiceLocator.Default.Resolve(type);
        }
    }
}