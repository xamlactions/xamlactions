using System;
using Windows.UI.Xaml.Data;

namespace XamlActions.Converters {
    public class OpacityConverter : IValueConverter {
        public double OpacityLevel { get; set; }

        public OpacityConverter() {
            OpacityLevel = 0.6;
        }

        public object Convert(object value, Type targetType, object parameter, string language) {
            return ((bool) value) ? OpacityLevel : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return (int) value == 1;
        }
    }
}