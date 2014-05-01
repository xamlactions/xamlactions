using Windows.UI.Xaml;

namespace XamlActions.Triggers {
    public abstract class Trigger : FrameworkElementCollection<TriggerAction> {
        public FrameworkElement ParentFrameworkElement { get; set; }
    }
}