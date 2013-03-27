using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace XamlActions.ViewServices {
    public class DispatcherHelper : IDispatcher {
        private static Dispatcher _coreDispatcher;

        public static Dispatcher Dispatcher {
            get {
                if (_coreDispatcher == null) {
                    if (Deployment.Current.Dispatcher == null) {
                        Debug.WriteLine("The Dispatcher is null. Try setting the Dispatcher calling 'DispatcherHelper.Dispatcher = Deployment.Current.Dispatcher;' in your App.xaml");
                        return null;
                    }
                    _coreDispatcher = Deployment.Current.Dispatcher;
                }
                return _coreDispatcher;
            }
            set { _coreDispatcher = value; }
        }

        public void Run(Action action) {
            if (Dispatcher == null || Dispatcher.CheckAccess()) {
                action.Invoke();
                return;
            }
            Dispatcher.BeginInvoke(action);
        }
    }
}