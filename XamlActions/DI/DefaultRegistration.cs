using XamlActions.Reflection;
using XamlActions.ViewServices;

namespace XamlActions.DI {
    public static class DefaultRegistration {

        private static bool _isRegistered;
        private static IServiceLocator _serviceLocator = XamlActions.DI.ServiceLocator.Default;

        public static IServiceLocator ServiceLocator {
            get { return _serviceLocator; }
            set { _serviceLocator = value; }
        }

        public static void EnsureRegistered() {
            if(_isRegistered) return;
            _isRegistered = true;

            ServiceLocator.Register<IDialogService>(PlatformResolver.ResolveImplementation<IDialogService>());
            ServiceLocator.Register<IDispatcher>(PlatformResolver.ResolveImplementation<IDispatcher>());
            ServiceLocator.Register<IDesignModeChecker>(PlatformResolver.ResolveImplementation<IDesignModeChecker>());
            ServiceLocator.Register<INavigationService>(PlatformResolver.ResolveImplementation<INavigationService>());
        }
    }
}
