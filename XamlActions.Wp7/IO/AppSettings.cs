using System.IO.IsolatedStorage;

namespace XamlActions.IO {
    public class AppSettings : IAppSettings {
        public object Get(string key) {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key)
                ? IsolatedStorageSettings.ApplicationSettings[key]
                : null;
        }

        public void Set(string key, object value) {
            IsolatedStorageSettings.ApplicationSettings[key] = value;
        }
    }
}