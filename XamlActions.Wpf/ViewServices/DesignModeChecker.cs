using System.ComponentModel;
using System.Windows;

namespace XamlActions.ViewServices {
    public class DesignModeChecker : IDesignModeChecker {
        public bool IsInDesignMode() {
            return DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }
    }
}
