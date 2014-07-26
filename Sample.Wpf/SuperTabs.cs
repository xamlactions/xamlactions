using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XamlActions.Actions;

namespace Sample.Wpf {
    public class SuperTabs : ObservableCollection<Tab> {
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            base.OnCollectionChanged(e);
            foreach (var item in e.NewItems) {
                var dp = (DependencyObject) item;
                
            }
        }

        public static readonly DependencyProperty TabsProperty = DependencyProperty.RegisterAttached(
            "TabsInternal",
            typeof(SuperTabs), 
            typeof (SuperTabs),
            new FrameworkPropertyMetadata());

        public static SuperTabs GetTabs(UIElement element) {
            var collection = (SuperTabs)element.GetValue(TabsProperty);
            if (collection == null) {
                collection = new SuperTabs();
                element.SetValue(TabsProperty, collection);
            }
            return collection;
        }

        public static void SetTabs(UIElement element, SuperTabs value) {
            element.SetValue(TabsProperty, value);
        }
    }

    public class Tab : DependencyObject {

        public string Nome {
            get { return (string)GetValue(NomeProperty); }
            set { SetValue(NomeProperty, value); }
        }

        public static readonly DependencyProperty NomeProperty =
            DependencyProperty.Register("Nome", typeof(string), typeof(Tab));

        
    }
}
