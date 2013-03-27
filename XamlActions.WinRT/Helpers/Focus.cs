using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XamlActions.Helpers {
    public static class Focus {
        public static readonly DependencyProperty MappingsProperty = DependencyProperty.RegisterAttached(
            "HasFocus",
            typeof (bool),
            typeof (Focus),
            null);

        public static bool GetHasFocus(DependencyObject dp) {
            return GetControl(dp).FocusState != FocusState.Unfocused;
        }

        public static void SetHasFocus(DependencyObject dp, bool value) {
            Control control = GetControl(dp);
            if (!value) return;
            control.Loaded += (sender, args) => control.Focus(FocusState.Programmatic);
        }

        private static Control GetControl(DependencyObject dp) {
            var control = dp as Control;
            if (control == null) {
                throw new ArgumentException("HasFocus can be only used in Controls");
            }
            return control;
        }
    }
}