using XamlActions.Data;
using XamlActions.Reflection;
using XamlActions.Tasks;
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

            RegisterIfFound<IDialogService>();
            RegisterIfFound<IDispatcher>();
            RegisterIfFound<IDesignModeChecker>();
            RegisterIfFound<INavigator>();
            RegisterIfFound<IAppSettings>();
            RegisterIfFound<IPhotoChooserTask>();
            RegisterIfFound<IReviewTask>();
            RegisterIfFound<IEmailTask>();
        }

        private static void RegisterIfFound<T>() {
            var type = PlatformResolver.ResolveImplementation<T>();
            if (type == null) {
                return;
            }
            ServiceLocator.Register<T>(type);
        }
    }
}
