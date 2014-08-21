using System.Windows;
using XamlActions.Helpers;
using XamlActions.Reflection;

namespace XamlActions.Triggers {
    public class EventFiredTrigger : Trigger {

        public string Event {
            get { return (string) GetValue(EventProperty); }
            set { SetValue(EventProperty, value); }
        }

        public static readonly DependencyProperty EventProperty =
            DependencyProperty.Register("Event", typeof (string), typeof (EventFiredTrigger),
                                        new PropertyMetadata("", PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject,
                                                    DependencyPropertyChangedEventArgs args) {
        }

        public EventFiredTrigger() {
            DependencyPropertyMonitor.MonitorForChanges(this, "DataContext", o => RegisterEvent());
        }

        private void RegisterEvent() {
            if (Event == null) return;
            EventHelper.Default.RegisterEvent(ParentFrameworkElement, Event, (s, a) => {
                foreach (TriggerAction action in Children) {
                    action.StartAction();
                }
            });
        }
    }
}