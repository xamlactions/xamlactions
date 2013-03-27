using System;
using System.Windows;
using Windows.UI.Popups;

namespace XamlActions.ViewServices {
    public class DialogService : IDialogService {
        private IDispatcher _dispatcher;

        public DialogService(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        public void ShowMessage(string message) {
            _dispatcher.Run(() => ShowMessageAsync(message));
        }

        private async void ShowMessageAsync(string message) {
        #if NETFX_CORE
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        #else 
            MessageBox.Show(message);
        #endif
        }
    }
}