using System;
using System.Windows;
#if NETFX_CORE
    using Windows.UI.Popups;
#endif

namespace XamlActions.ViewServices {
    public class DialogService : IDialogService {
        private IDispatcher _dispatcher;

        public DialogService(IDispatcher dispatcher) {
            _dispatcher = dispatcher;
        }

        public void ShowMessage(string message) {
            _dispatcher.Run(() => ShowMessageAsync(message));
        }

        
        #if NETFX_CORE
        private async void ShowMessageAsync(string message) {
            var dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }
        #else
        private void ShowMessageAsync(string message) {
            MessageBox.Show(message);
        }
#endif
        
    }
}