using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamlActions.ViewServices {
    public class Navigator : INavigator {
        private static Dictionary<string, Type> _mapping = new Dictionary<string, Type>();

        private readonly IDispatcher _dispatcher;
        private Frame _mainFrame;

        private Frame MainFrame {
            get { return _mainFrame ?? (_mainFrame = Window.Current.Content as Frame); }
        }

        public Navigator(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        public void NavigateTo(string viewName) {
            if (!_mapping.ContainsKey(viewName)) {
                throw new KeyNotFoundException("Type for view " + viewName + " not found. Try registering before using Navigator.RegisterView");
            }
            _dispatcher.Run(() => MainFrame.Navigate(_mapping[viewName]));
        }

        public Type GetCurrentViewType() {
            return _mainFrame == null ? null : _mainFrame.CurrentSourcePageType;
        }

        public void GoBack() {
            _dispatcher.Run(() => MainFrame.GoBack());
        }

        public static void RegisterView(string viewName, Type type) {
            _mapping[viewName] = type;
        }
    }
}