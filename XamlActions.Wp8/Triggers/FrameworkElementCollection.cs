using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace XamlActions.Triggers {
  
        public class FrameworkElementCollection<T> : FrameworkElement, IList<T>, INotifyCollectionChanged, IList
            where T : FrameworkElement {
           
            private ObservableCollection<T> _items = new ObservableCollection<T>();

            public FrameworkElementCollection() {
                _items.CollectionChanged += Items_CollectionChanged;
            }
          
            private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
                OnCollectionChanged(e);
            }

            public void Add(T item) {
                item.DataContext = DataContext;
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
                item.DataContext = DataContext;
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

            int IList.Add(object value) {
                Add((T)value);
                return 1;
            }

            void IList.Clear() {
                Clear();
            }

            bool IList.Contains(object value) {
                return Contains((T)value);
            }

            int IList.IndexOf(object value) {
                return IndexOf((T) value);
            }

            void IList.Insert(int index, object value) {
                Insert(index, (T)value);
            }

            bool IList.IsFixedSize {
                get { return false; }
            }

            bool IList.IsReadOnly {
                get { return IsReadOnly; }
            }

            void IList.Remove(object value) {
                Remove((T) value);
            }

            void IList.RemoveAt(int index) {
                RemoveAt(index);
            }

            object IList.this[int index] {
                get {
                    return this[index];
                }
                set { this[index] = (T)value; }
            }

            void ICollection.CopyTo(System.Array array, int index) {
                CopyTo(array.Cast<T>().ToArray(), index);
            }

            int ICollection.Count {
                get { return Count; }
            }

            bool ICollection.IsSynchronized {
                get { throw new System.NotImplementedException(); }
            }

            object ICollection.SyncRoot {
                get { throw new System.NotImplementedException(); }
            }
        }
    }