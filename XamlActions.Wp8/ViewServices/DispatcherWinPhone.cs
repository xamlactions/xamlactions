using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace XamlActions.ViewServices {
    public class DispatcherWinPhone : IDispatcher {
        private static Dispatcher _coreDispatcher;

        public static Dispatcher Dispatcher {
            get {
                if (_coreDispatcher == null) {
                    if (Deployment.Current.Dispatcher == null) {
                        Debug.WriteLine("The Dispatcher is null. Try setting the Dispatcher calling 'DispatcherWinPhone.Dispatcher = Deployment.Current.Dispatcher;' in your App.xaml");
                        return null;
                    }
                    _coreDispatcher = Deployment.Current.Dispatcher;
                }
                return _coreDispatcher;
            }
            set { _coreDispatcher = value; }
        }

        public async Task Run(Action action) {
            if (Dispatcher == null || Dispatcher.CheckAccess()) {
                action.Invoke();
                return;
            }
            await Task.Run(() => Dispatcher.BeginInvoke(action));
        }
    }
}