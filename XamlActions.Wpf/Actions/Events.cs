using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using XamlActions.Reflection;

namespace XamlActions.Actions {
    public class Events : ObservableCollection<Map> {
        public FrameworkElement ParentFrameworkElement { get; set; }

        public static readonly DependencyProperty MappingsProperty = DependencyProperty.RegisterAttached(
            "MappingsInternal",
            typeof(Events),
            typeof(Events),
            new FrameworkPropertyMetadata());

        public static Events GetMappings(DependencyObject obj) {
            var eventsCollection = obj.GetValue(MappingsProperty) as Events;
            if (eventsCollection == null) {
                eventsCollection = new Events();
                SetMappings(obj, eventsCollection);
            }
            return eventsCollection;
        }

        public static void SetMappings(DependencyObject obj, Events events) {
            obj.SetValue(MappingsProperty, events);
            var element = obj as FrameworkElement;
            if (element == null) {
                throw new ArgumentException("Mappings need to be attached to a FrameworkElement");
            }
            events.ParentFrameworkElement = element;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            base.OnCollectionChanged(e);
            if (e.NewItems != null && e.NewItems.Count > 0) {
                RegisterEventToMaps(e.NewItems.Cast<Map>().ToList());
            }
            if (e.OldItems != null && e.OldItems.Count > 0) {
                UnregisterEventFromMaps(e.OldItems);
            }
        }

        private void RegisterEventToMaps(IEnumerable<Map> maps) {
            if (maps == null) return;
            foreach (Map map in maps) {
                Map localMap = map;
                EventHelper.Default.RegisterEvent(ParentFrameworkElement, map.Event, (s, a) => EventFired(localMap, a));
            }
        }

        private void UnregisterEventFromMaps(IList maps) {
            if (maps == null) return;
            foreach (Map map in maps) {
                Map localMap = map;
                EventHelper.Default.UnregisterEvent(ParentFrameworkElement, map.Event, (s, a) => EventFired(localMap, a));
            }
        }

        public void EventFired(Map map, object eventArgs) {
            if (map == null) return;
            map.DataContext = ParentFrameworkElement.DataContext;
            object dataContext = GetDataContext(map, ParentFrameworkElement);
            string methodName = map.ToMethod;
            CallMethod(map, dataContext, methodName, eventArgs);
        }

        private static object GetDataContext(Map map, DependencyObject dependencyObject) {
            if (map.OfDataContext == null && !(dependencyObject is FrameworkElement)) {
                throw new NotSupportedException("When not a framework element, you must set the parameter OfDataContext. Error trying to map event [" + map.Event + "] to method [" + map.ToMethod + "]. Object: " + dependencyObject);
            }
            return map.OfDataContext ?? dependencyObject.GetValue(FrameworkElement.DataContextProperty);
        }

        private static void CallMethod(Map map, object dataContext, string methodName, object eventArgs) {
            var parameters = new List<object>();
            if (map.SendingEventArgs) {
                parameters.Add(eventArgs);
            }
            if (map.HasParam) {
                parameters.Add(map.WithParam);
            }
            try {
                Reflector.CallMethod(dataContext, methodName, parameters.ToArray());
            }
            catch (ReflectorException ex) {
                Debug.WriteLine("WARN: " + ex.Message);
            }
        }
    }
}
