using Windows.UI.Xaml;

namespace XamlActions.Converters {
    public class TrueIsVisibleConverter : BoolToValueConverter<Visibility> {
        public TrueIsVisibleConverter() {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }
    }
}