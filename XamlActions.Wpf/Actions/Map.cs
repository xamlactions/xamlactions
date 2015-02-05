using System.Windows;

namespace XamlActions.Actions {
    public class Map : Freezable {
        public string Event { get; set; }
        public string ToMethod { get; set; }
        public bool SendingEventArgs { get; set; }
        public bool HasParam { get; protected set; }

        public static readonly DependencyProperty WithParamProperty =
            DependencyProperty.Register("WithParam",
                                        typeof(object),
                                        typeof(Map),
                                        new PropertyMetadata(null, OnWithParamChanged));

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
                                        new PropertyMetadata(null, OnDataContextChanged));

        private static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        }

        public static void SetOfDataContext(DependencyObject dependencyObject, string value) {
            dependencyObject.SetValue(OfDataContextProperty, value);
        }

        protected override Freezable CreateInstanceCore() {
            throw new System.NotImplementedException();
        }
    }
}
