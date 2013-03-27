using System.ComponentModel;

namespace XamlActions.ViewServices {
    public class DesignModeChecker : IDesignModeChecker {
        public bool IsInDesignMode() {
            return DesignerProperties.IsInDesignTool;
        }
    }
}
