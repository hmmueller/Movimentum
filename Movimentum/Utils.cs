using System;
using System.Collections.Generic;
using System.Linq;

namespace Movimentum {
    public static class DictionaryUtils {
        public static IDictionary<TKey, TValue> Empty<TKey, TValue>() {
            return new Dictionary<TKey, TValue>();
        }
        public static TValue Of<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue) {
            TValue result;
            return dictionary.TryGetValue(key, out result) ? result : defaultValue;
        }

        public static IDictionary<TKey, TValue> Union<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) {
            return new Dictionary<TKey, TValue>(dictionary) { { key, value } }; // eager full copy; lazy Dict has no n^2 problem, but is more work.
        }
    }

    public static class LinqUtils {
        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T item) {
            return enumerable.Except(new[] { item });
        }
    }

    public static class DoubleUtils {
        public static bool Near(this double d1, double d2) {
            return Math.Abs(d1 - d2) < 1e-8; // ????
        }
        public static bool RoughNear(this double d1, double d2) {
            return Math.Abs(d1 - d2) < 1e-2; // ????
        }
    }
}
