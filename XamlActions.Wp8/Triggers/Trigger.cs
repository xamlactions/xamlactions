using System.Windows;
using System.Windows.Markup;

namespace XamlActions.Triggers {
    [ContentProperty("Children")]
    public abstract class Trigger : FrameworkElement {
        private TriggerActionCollection _children;
        public FrameworkElement ParentFrameworkElement { get; set; }

        public TriggerActionCollection Children {
            get { return _children ?? (_children = new TriggerActionCollection()); }
            set { _children = value; }
        }
    }

    public class TriggerActionCollection : FrameworkElementCollection<TriggerAction> { }
}