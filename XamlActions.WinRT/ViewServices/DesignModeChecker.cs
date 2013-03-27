namespace XamlActions.ViewServices {
    public class DesignModeChecker : IDesignModeChecker {
        public bool IsInDesignMode() {
            return Windows.ApplicationModel.DesignMode.DesignModeEnabled;
        }
    }
}
