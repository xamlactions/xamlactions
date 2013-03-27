using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if NETFX_CORE
    using Windows.UI.Xaml;
#else
    using System.Windows;
#endif

namespace XamlActions.Actions {
    public class Map : FrameworkElement {
        public string Event { get; set; }
        public string ToMethod { get; set; }
        public bool SendingEventArgs { get; set; }

        public bool HasParam { get; protected set; }

        public static readonly DependencyProperty WithParamProperty =
            DependencyProperty.Register("WithParam",
                                        typeof(object),
                                        typeof(Map),
                                        new PropertyMetadata(DependencyProperty.UnsetValue, OnWithParamChanged));

        private static void OnWithParamChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var map = d as Map;
            if (map == null) return;
            map.HasParam = true;
        }

        public object WithParam {
            get { return GetValue(WithParamProperty); }
            set { SetValue(WithParamProperty, value); }
        }

        public static void SetWithParam(DependencyObject dependencyObject, string value) {
            dependencyObject.SetValue(WithParamProperty, value);
        }

        public object OfDataContext {
            get {
                return GetValue(OfDataContextProperty);
            }
            set {
                SetValue(OfDataContextProperty, value);
            }
        }

        public static readonly DependencyProperty OfDataContextProperty =
            DependencyProperty.Register("OfDataContext",
                                        typeof(object),
                                        typeof(Map),
                                        new PropertyMetadata(DependencyProperty.UnsetValue, OnDataContextChanged));

        private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        }

        public static void SetOfDataContext(DependencyObject dependencyObject, string value) {
            dependencyObject.SetValue(OfDataContextProperty, value);
        }
    }
}
