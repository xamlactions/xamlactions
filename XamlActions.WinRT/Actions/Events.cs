using System;
#if NETFX_CORE
    using Windows.UI.Xaml;
#else 
    using System.Windows;
#endif

namespace XamlActions.Actions {
	public static class Events {

        public static readonly DependencyProperty MappingsProperty = DependencyProperty.RegisterAttached(
            "Mappings",
            typeof(EventsCollection),
            typeof(Events),
            null);

		public static EventsCollection GetMappings(DependencyObject obj) {
		    var eventsCollection = obj.GetValue(MappingsProperty) as EventsCollection;
            if(eventsCollection == null) {
                eventsCollection = new EventsCollection();
                SetMappings(obj, eventsCollection);
            }
		    return eventsCollection;
		}

		public static void SetMappings(DependencyObject obj, EventsCollection value) {
			obj.SetValue(MappingsProperty, value);
		    var element = obj as FrameworkElement;
            if(element == null) {
                throw new ArgumentException("Mappings need to be attached to a FrameworkElement");
            }
            value.SetParentFrameworkElement(element);
		}
	}
}
