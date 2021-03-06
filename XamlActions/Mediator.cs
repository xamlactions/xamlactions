﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace XamlActions {
    public class Mediator : IMediator {

        public static IMediator Default = new Mediator();

        private static object _sync = new object();

        private Dictionary<Type, List<WeakDelegate>> _subscribers = new Dictionary<Type, List<WeakDelegate>>();

        public void Subscribe<T>(object subscriber, Action<T> action) {
            lock (_sync) {
                EnsureSubscribersList<T>();
                _subscribers[typeof(T)].Add(new WeakDelegate(subscriber, action));
            }
        }

        private void EnsureSubscribersList<T>() {
            if (!_subscribers.ContainsKey(typeof(T))) {
                _subscribers.Add(typeof(T), new List<WeakDelegate>());
            }
        }

        public void Publish(object message) {
            lock (_sync) {
                var type = message.GetType();
                if (!_subscribers.ContainsKey(type)) return;
                var alive = _subscribers[type].Where(x => x.IsAlive).ToList();
                foreach (var weakAction in alive) {
                    weakAction.Execute(message);
                }
                _subscribers[type] = alive;
            }
        }

        public void Unsubscribe<T>(object subscriber) {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type)) return;
            foreach (var wd in _subscribers[type].Where(wd => wd.Target == subscriber)) {
                wd.Target = null;
            }
        }

        public int RegisteredSubscribers() {
            lock (_sync) {
                return _subscribers.Sum(x => x.Value != null ? x.Value.Count : 0);
            }
        }
    }
}