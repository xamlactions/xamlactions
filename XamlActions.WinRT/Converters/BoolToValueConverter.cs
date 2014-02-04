using System;
using Windows.UI.Xaml.Data;

namespace XamlActions.Converters {
    public class BoolToValueConverter<T> : IValueConverter {
        public T FalseValue { get; set; }
        public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, string culture) {
            if (value == null)
                return FalseValue;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture) {
            return value != null && value.Equals(TrueValue);
        }
    }
}
