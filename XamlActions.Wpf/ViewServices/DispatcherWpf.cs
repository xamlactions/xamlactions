using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace XamlActions.ViewServices {
    public class DispatcherWinPhone : IDispatcher {
        private static Dispatcher _dispatcher;

        public static Dispatcher Dispatcher {
            get {
                if (_dispatcher == null) {
                    _dispatcher = Application.Current.Dispatcher;                    
                }
                return _dispatcher;
            }
            set { _dispatcher = value; }
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