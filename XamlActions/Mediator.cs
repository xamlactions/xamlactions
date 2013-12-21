using System;
using System.Collections.Generic;
using System.Linq;

namespace XamlActions {
    public class Mediator : IMediator {

        public static IMediator Default = new Mediator();

        private static object _sync = new object();

        private Dictionary<Type, List<WeakReference>> _subscribers = new Dictionary<Type, List<WeakReference>>();

        public void Subscribe<T>(Action<T> action) {
            lock (_sync) {
                if (!_subscribers.ContainsKey(typeof(T))) {
                    _subscribers.Add(typeof(T), new List<WeakReference>());
                }
                _subscribers[typeof(T)].Add(new WeakReference(action));
            }
        }

        public void Publish(object message) {
            lock (_sync) {
                var type = message.GetType();
                if (!_subscribers.ContainsKey(type)) return;
                var alive = new List<WeakReference>();
                foreach (var sub in _subscribers[type].Where(sub => sub.IsAlive)) {
                    ((Delegate)sub.Target).DynamicInvoke(message);
                    alive.Add(sub);
                }
                _subscribers[type] = alive;
            }
        }

        public int RegisteredSubscribers() {
            lock (_sync) {
                return _subscribers.Sum(x => x.Value != null ? x.Value.Count : 0);
            }
        }
    }
}