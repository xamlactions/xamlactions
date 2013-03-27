using System;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.Xaml;


namespace XamlActions.ViewServices {
    public class DispatcherHelper : IDispatcher {
        private static CoreDispatcher _coreDispatcher;

        public static CoreDispatcher Dispatcher {
            get {
                if (_coreDispatcher == null) {
                    if (Window.Current.Dispatcher == null) {
                        Debug.WriteLine("The Dispatcher is null. Try setting the Dispatcher calling 'DispatcherHelper.Dispatcher = Window.Current.Dispatcher;' in your App.xaml");
                        return null;
                    }
                    _coreDispatcher = Window.Current.Dispatcher;
                }
                return _coreDispatcher;
            }
            set { _coreDispatcher = value; }
        }

        public void Run(Action action) {
            if (Dispatcher == null || Dispatcher.HasThreadAccess) {
                action.Invoke();
                return;
            }
            Dispatcher.RunAsync(CoreDispatcherPriority.Normal, action.Invoke);
        }
    }
}