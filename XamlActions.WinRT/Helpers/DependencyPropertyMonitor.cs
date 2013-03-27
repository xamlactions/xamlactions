using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace XamlActions.Helpers {
    public static class DependencyPropertyMonitor {
        public static void MonitorForChanges(FrameworkElement element, string property, Action<object> onPropertyChanged) {
            var myDataContextProperty =
                DependencyProperty.Register("Monitor" + property, typeof(object), typeof(DependencyPropertyMonitor),
                                            new PropertyMetadata(null, (o, args) => onPropertyChanged.Invoke(args.NewValue)));

            var binding = new Binding();
            binding.Path = new PropertyPath(property);
            binding.Source = element;
            BindingOperations.SetBinding(element, myDataContextProperty, binding);
        }
    }
}
