﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.UI.Xaml;

namespace XamlActions {
    public class DependencyObjectCollection<T> : DependencyObject, IList<T>, INotifyCollectionChanged
        where T : DependencyObject {
        private ObservableCollection<T> _items = new ObservableCollection<T>();

        public DependencyObjectCollection() {
            _items.CollectionChanged += ItemsCollectionChanged;
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            OnCollectionChanged(e);
        }

        public void Add(T item) {
            _items.Add(item);
        }

        public void Clear() {
            _items.Clear();
        }

        public bool Contains(T item) {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            _items.CopyTo(array, arrayIndex);
        }

        public int Count {
            get { return _items.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public bool Remove(T item) {
            return _items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator() {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _items.GetEnumerator();
        }

        public int IndexOf(T item) {
            return _items.IndexOf(item);
        }

        public void Insert(int index, T item) {
            _items.Insert(index, item);
        }

        public void RemoveAt(int index) {
            _items.RemoveAt(index);
        }

        public T this[int index] {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args) {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null) {
                handler(this, args);
            }
        }
    }
}