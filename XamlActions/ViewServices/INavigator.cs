namespace XamlActions.ViewServices {
    public interface INavigator {
        void NavigateTo(string viewName);
        void GoBack();
    }
}