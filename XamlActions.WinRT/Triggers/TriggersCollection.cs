using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace XamlActions.Triggers {
    public class TriggersCollection : DependencyObjectCollection<Trigger> {
        private FrameworkElement _parentFrameworkElement;

        public void SetDependencyObject(FrameworkElement parentFrameworkElement) {
            _parentFrameworkElement = parentFrameworkElement;
            if(_parentFrameworkElement.DataContext == null) {
                _parentFrameworkElement.Loaded += ParentFrameworkElementLoaded;
            }
            CollectionChanged += OnCollectionChanged;
        }

        private void ParentFrameworkElementLoaded(object sender, RoutedEventArgs e) {
            SetDataContextToChildren(this);
            _parentFrameworkElement.Loaded -= ParentFrameworkElementLoaded;
        }

        private void SetDataContextToChildren(IEnumerable<Trigger> triggers) {
            foreach (Trigger trigger in triggers) {
                trigger.ParentFrameworkElement = _parentFrameworkElement;
                trigger.DataContext = _parentFrameworkElement.DataContext;
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if (e.NewItems != null) {
                SetDataContextToChildren(e.NewItems.Cast<Trigger>());
            }
        }
    }
}