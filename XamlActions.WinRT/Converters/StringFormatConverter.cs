using System;
using Windows.UI.Xaml.Data;

namespace XamlActions.Converters {
    public class StringFormatConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, string language) {
            if (parameter == null) {
                return value;
            }
            return String.Format((String)parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            return value;
        }
    }
}
