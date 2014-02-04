using Windows.UI.Xaml;

namespace XamlActions.Converters {
    public class FalseIsVisibleConverter : BoolToValueConverter<Visibility> {
        public FalseIsVisibleConverter() {
            TrueValue = Visibility.Collapsed;
            FalseValue = Visibility.Visible;
        }
    }
}