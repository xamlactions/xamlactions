using System.Windows;

namespace XamlActions.Triggers {
    public class PropertyChangedTrigger : Trigger {

        public object Binding {
            get {
                return GetValue(BindingProperty);
            }
            set {
                SetValue(BindingProperty, value);
            }
        }

        public static readonly DependencyProperty BindingProperty =
            DependencyProperty.Register("Binding",
                                        typeof(object),
                                        typeof(PropertyChangedTrigger),
                                        new PropertyMetadata(null, OnBindingChange));

        private static void OnBindingChange(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var trigger = ((PropertyChangedTrigger)d);
            if (e.NewValue == e.OldValue) return;

            foreach (TriggerAction action in trigger.Children) {
                action.StartAction();
            }
        }
    }
}
