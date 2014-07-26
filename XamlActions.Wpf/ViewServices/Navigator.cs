using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;

namespace XamlActions.ViewServices {
    public class Navigator : INavigator {
        private static Dictionary<string, Uri> _mapping = new Dictionary<string, Uri>();

        private readonly IDispatcher _dispatcher;
        private NavigationService _mainFrame;

        private NavigationService MainWindow {
            get {
                return _mainFrame ?? (_mainFrame = NavigationService.GetNavigationService(Application.Current.MainWindow));
            }
        }

        public Navigator(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        public void NavigateTo(string viewName) {
            if (!_mapping.ContainsKey(viewName)) {
                throw new KeyNotFoundException("Uri for view " + viewName + " not found. Try registering before using Navigator.RegisterView");
            }
            _dispatcher.Run(() => MainWindow.Navigate(_mapping[viewName]));
        }

        public string GetCurrentViewKey() {
            return _mainFrame == null ? null : _mapping.FirstOrDefault(x => x.Value == _mainFrame.CurrentSource).Key;
        }

        public void GoBack() {
            _dispatcher.Run(() => MainWindow.GoBack());
        }

        public static void RegisterView(string viewName, Uri address) {
            _mapping[viewName] = address;
        }
    }
}