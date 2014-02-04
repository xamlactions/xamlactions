namespace XamlActions.ViewServices {
    public interface INavigator {
        void NavigateTo(string viewName);
        string GetCurrentViewKey();
        void GoBack();
    }
}