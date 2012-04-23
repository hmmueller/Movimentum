using System.Collections.Generic;

namespace Movimentum.Model {
    public interface IReadOnlyDictionary<TKey, TValue> {
        IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();
        bool Contains(KeyValuePair<TKey, TValue> item);
        void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex);
        int Count { get; }
        IEnumerable<TKey> Keys { get; }
        IEnumerable<TValue> Values { get; }
        bool ContainsKey(TKey key);
        bool TryGetValue(TKey key, out TValue value);
        TValue this[TKey key] { get; }
    }

    public class ReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue> {
        private readonly IDictionary<TKey, TValue> _data;

        public ReadOnlyDictionary(IDictionary<TKey, TValue> data) {
            _data = data;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            return _data.GetEnumerator();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item) {
            return _data.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            _data.CopyTo(array, arrayIndex);
        }

        public int Count {
            get { return _data.Count; }
        }

        public bool ContainsKey(TKey key) {
            return _data.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TValue value) {
            return _data.TryGetValue(key, out value);
        }

        public TValue this[TKey key] {
            get { return _data[key]; }
        }

        public IEnumerable<TKey> Keys {
            get { return _data.Keys; }
        }

        public IEnumerable<TValue> Values {
            get { return _data.Values; }
        }
    }
}