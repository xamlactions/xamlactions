using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;


namespace XamlActions.ViewServices {
    public class DispatcherWinRT : IDispatcher {
        private static CoreDispatcher _coreDispatcher;

        public static CoreDispatcher Dispatcher {
            get {
                if (_coreDispatcher == null) {
                    try {
                        _coreDispatcher = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher;
                    }
                    catch {
                        Debug.WriteLine("The Dispatcher is null. Try setting the Dispatcher calling 'DispatcherWinRT.Dispatcher = Window.Current.Dispatcher;' in your App.xaml");
                        return null;
                    }
                }
                return _coreDispatcher;
            }
            set { _coreDispatcher = value; }
        }

        public async Task Run(Action action) {
            if (Dispatcher == null || Dispatcher.HasThreadAccess) {
                action.Invoke();
                return;
            }
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, action.Invoke);
        }
    }
}