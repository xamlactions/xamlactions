using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using XamlActions.Reflection;
using XamlActions.ViewServices;

namespace XamlActions.Actions {
    public class Events : FreezableCollection<Map> {
        public FrameworkElement ParentFrameworkElement { get; set; }
        public List<Map> Maps { get; private set; }

        public Events() {
            Maps = new List<Map>();
        }

        static Events() {
            try {
                MappingsProperty =
                DependencyProperty.RegisterAttached(
                    "MappingsInternal123",
                    typeof(Events),
                    typeof(DependencyObject),
                    new PropertyMetadata(OnX));
                
            }
            catch{}
        }

        public static readonly DependencyProperty MappingsProperty;

        private static void OnX(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        }

        public static Events GetMappings(DependencyObject element) {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) {
                return new Events();
            }
            var eventsCollection = element.GetValue(MappingsProperty) as Events;
            if (eventsCollection == null) {
                eventsCollection = new Events();
                eventsCollection.Changed += EventsCollectionOnChanged;
                SetMappings(element, eventsCollection);
            }
            return eventsCollection;
        }

        private static void EventsCollectionOnChanged(object sender, EventArgs eventArgs) {
            var events = sender as Events;
            if (events == null) {
                return;
            }
            var newItems = events.TakeWhile(x => !events.Maps.Contains(x));
            var oldItems = events.Maps.TakeWhile(x => !events.Contains(x));

            events.RegisterEventToMaps(newItems.ToList());
            events.UnregisterEventFromMaps(oldItems);
        }

        public static void SetMappings(DependencyObject obj, Events events) {
            obj.SetValue(MappingsProperty, events);
            var element = obj as FrameworkElement;
            if (element == null) {
                throw new ArgumentException("Mappings need to be attached to a FrameworkElement");
            }
            events.ParentFrameworkElement = element;
        }

        private void RegisterEventToMaps(IEnumerable<Map> maps) {
            if (maps == null) return;
            foreach (Map map in maps) {
                Map localMap = map;
                EventHelper.Default.RegisterEvent(ParentFrameworkElement, map.Event, (s, a) => EventFired(localMap, a));
            }
        }

        private void UnregisterEventFromMaps(IEnumerable<Map> maps) {
            if (maps == null) return;
            foreach (Map map in maps) {
                Map localMap = map;
                EventHelper.Default.UnregisterEvent(ParentFrameworkElement, map.Event, (s, a) => EventFired(localMap, a));
            }
        }

        public void EventFired(Map map, object eventArgs) {
            if (map == null) return;
            //map.DataContext = ParentFrameworkElement.DataContext;
            object dataContext = GetDataContext(map, ParentFrameworkElement);
            string methodName = map.ToMethod;
            CallMethod(map, dataContext, methodName, eventArgs);
        }

        private static object GetDataContext(Map map, DependencyObject dependencyObject) {
            if (map.OfDataContext == null && !(dependencyObject is FrameworkElement)) {
                throw new NotSupportedException(
                    "When not a framework element, you must set the parameter OfDataContext. Error trying to map event [" +
                    map.Event + "] to method [" + map.ToMethod + "]. Object: " + dependencyObject);
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