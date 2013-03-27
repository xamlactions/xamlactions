using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace XamlActions.Converters {
    public class CustomColorConverter : IValueConverter {
        private SolidColorBrush _normal;
        private SolidColorBrush _selected;
        public string SelectedBrush { get; set; }
        public string RegularBrush { get; set; }

        protected SolidColorBrush Normal {
            get {
                if (_normal == null) {
                    _normal = (SolidColorBrush) Application.Current.Resources[RegularBrush];
                }
                return _normal;
            }
            set { _normal = value; }
        }

        protected SolidColorBrush Selected {
            get {
                if (_selected == null) {
                    _selected = (SolidColorBrush) Application.Current.Resources[SelectedBrush];
                }
                return _selected;
            }
            set { _selected = value; }
        }

        public CustomColorConverter() {
            if (String.IsNullOrEmpty(SelectedBrush)) {
                SelectedBrush = "AppAccentColorBrush";
            }
            if (String.IsNullOrEmpty(RegularBrush)) {
                RegularBrush = "ApplicationForegroundThemeBrush";
            }
        }

        public object Convert(object value, Type targetType, object parameter, string culture) {
            return ((bool) value) ? Selected : Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture) {
            throw new NotImplementedException();
        }
    }
}