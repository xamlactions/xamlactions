using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace XamlActions.ViewServices {
    public class Navigator : INavigator {
        private static Dictionary<string, Uri> _mapping = new Dictionary<string, Uri>();

        private readonly IDispatcher _dispatcher;
        private Frame _mainFrame;

        private Frame MainFrame {
            get { return _mainFrame ?? (_mainFrame = Application.Current.RootVisual as Frame); }
        }

        public Navigator(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        public void NavigateTo(string viewName) {
            if (!_mapping.ContainsKey(viewName)) {
                throw new KeyNotFoundException("Uri for view " + viewName + " not found. Try registering before using Navigator.RegisterView");
            }
            _dispatcher.Run(() => MainFrame.Navigate(_mapping[viewName]));
        }

        public Type GetCurrentViewType() {
            return null;
        }

        public void GoBack() {
            _dispatcher.Run(() => MainFrame.GoBack());
        }

        public static void RegisterView(string viewName, Uri address) {
            _mapping[viewName] = address;
        }
    }
}