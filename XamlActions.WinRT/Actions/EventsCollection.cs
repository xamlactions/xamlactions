using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using XamlActions.Reflection;
#if NETFX_CORE
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;
    using XamlActions;
    using XamlActions.Actions;
    using XamlActions.Reflection;
#else
    using System.Windows;
    using System.Windows.Data;
#endif

namespace XamlActions.Actions {
	public class EventsCollection : DependencyObjectCollection<Map> {
		private FrameworkElement _parentFrameworkElement;

		public EventsCollection() {
            //Debugger.Break();
		    //if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
		}

		public void SetParentFrameworkElement(FrameworkElement parent) {
            if (ViewModelBase.IsInDesignMode()) return;
			_parentFrameworkElement = parent;
		    MonitorForDataContextChanges(_parentFrameworkElement);
			RegisterEventToMaps(this);
			CollectionChanged += OnCollectionChanged;
		}

	    private void MonitorForDataContextChanges(FrameworkElement parentFrameworkElement) {
            var myDataContextProperty =
                DependencyProperty.Register("MyDataContext", typeof(object), GetType(),
                                            new PropertyMetadata(null, (o, args) => {
                                                foreach (Map map in this) {
                                                    map.DataContext = args.NewValue;
                                                }
                                            }));

            var binding = new Binding();
            binding.Path = new PropertyPath("DataContext");
            binding.Source = _parentFrameworkElement;
            BindingOperations.SetBinding(this, myDataContextProperty, binding);
	    }

	    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(e.NewItems != null && e.NewItems.Count > 0) {
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
                EventHelper.RegisterEvent(_parentFrameworkElement, map.Event, (s, a) => EventFired(localMap, a));
			}
		}
		
		private void UnregisterEventFromMaps(IList maps) {
			if (maps == null) return;
			foreach (Map map in maps) {
				Map localMap = map;
                EventHelper.UnregisterEvent(_parentFrameworkElement, map.Event, (s, a) => EventFired(localMap, a));
			}
		}

		public void EventFired(Map map, object eventArgs) {
            //Without this line we can have exceptions in design mode
#if SILVERLIGHT 
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this)) return;
#endif
            if(map == null) return;
		    map.DataContext = _parentFrameworkElement.DataContext;
			object dataContext = GetDataContext(map, _parentFrameworkElement);
			string methodName = map.ToMethod;
			CallMethod(map, dataContext, methodName, eventArgs);
		}

		private static object GetDataContext(Map map, DependencyObject dependencyObject) {
            if(map.OfDataContext == null && !(dependencyObject is FrameworkElement)) {
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
			catch(ReflectorException ex) {
				Debug.WriteLine("WARN: " + ex.Message);
			}
		}
	}
}