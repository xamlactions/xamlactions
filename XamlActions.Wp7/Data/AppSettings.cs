using System.IO.IsolatedStorage;

namespace XamlActions.Data {
    public class AppSettings : IAppSettings {
        public object Get(string key) {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key)
                ? IsolatedStorageSettings.ApplicationSettings[key]
                : null;
        }

        public void Set(string key, object value) {
            IsolatedStorageSettings.ApplicationSettings[key] = value;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}