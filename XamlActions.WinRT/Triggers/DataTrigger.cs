using Windows.UI.Xaml;

namespace XamlActions.Triggers {
    public class DataTrigger : Trigger {
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
                                        typeof (object),
                                        typeof (DataTrigger),
                                        new PropertyMetadata(null, OnBindingChange));

        private static void OnBindingChange(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var trigger = ((DataTrigger) d);
            if(e.NewValue == null || !e.NewValue.Equals(trigger.Value)) return;

            foreach (TriggerAction action in trigger) {
                action.StartAction();
            }
        }


        public object Value {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value",
                                        typeof (object),
                                        typeof (DataTrigger),
                                        new PropertyMetadata(null, OnValueChange));

        private static void OnValueChange(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            
        }
    }
}