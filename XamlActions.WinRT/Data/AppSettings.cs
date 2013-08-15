using Windows.Storage;

namespace XamlActions.Data {
    public class AppSettings : IAppSettings {
        private ApplicationDataContainer _container;

        public AppSettings() {
            if (!ApplicationData.Current.LocalSettings.Containers.ContainsKey("AppSettings")) {
                _container = ApplicationData.Current.LocalSettings.CreateContainer("AppSettings",
                    ApplicationDataCreateDisposition.Always);
            }
        }

        public object Get(string key) {
            return _container.Values.ContainsKey(key)
                ? _container.Values[key]
                : null;
        }

        public void Set(string key, object value) {
            _container.Values[key] = value;
        }
    }
}