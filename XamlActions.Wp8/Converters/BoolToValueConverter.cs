using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace XamlActions.Converters {
    public class BoolToValueConverter<T> : IValueConverter {
        public T FalseValue { get; set; }
        public T TrueValue { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) {
                return FalseValue;
            }
            return (bool) value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value != null && value.Equals(TrueValue);
        }
    }

    public class BoolToVisibilityConverter : BoolToValueConverter<Visibility> {}
}