namespace XamlActions.ViewServices {
    public interface INavigationService {
        void NavigateTo(string viewName);
        void GoBack();
    }
}