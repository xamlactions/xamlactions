using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using XamlActions.Reflection;

namespace XamlActions.Actions {
    public class ApplicationBarAction : ApplicationBarIconButton {
        public string OnClickAction { get; set; }

        public ApplicationBarAction() {
            Click += (s, e) => CallAction(OnClickAction, s, e);
        }

        private static void CallAction(string action, object sender, EventArgs e) {
            var mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (mainFrame == null) {
                Debug.WriteLine("ApplicationBarIconButtonEx -> Could not find PhoneApplicationFrame");
                return;
            }
            var content = mainFrame.Content as Control;
            if (content == null) {
                Debug.WriteLine("ApplicationBarIconButtonEx -> PhoneApplicationFrame.Content is not a control!");
                return;
            }
            if (content.DataContext == null) {
                Debug.WriteLine("ApplicationBarIconButtonEx -> PhoneApplicationFrame.Content.DataContext is null!");
                return;
            }
            object dataContext = content.DataContext;
            if (action == null) {
                Debug.WriteLine("ApplicationBarIconButtonEx -> No action to perform");
                return;
            }
            try {
                Reflector.CallMethod(dataContext, action);
            }
            catch {
                Debug.WriteLine("ApplicationBarIconButtonEx -> Could not find method {0} in object {1}", action, dataContext);
            }
        }
    }
}