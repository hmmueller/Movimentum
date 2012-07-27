using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static double NearSqrt(this double d) {
            return d.Near(0) ? 0 : Math.Sqrt(d);
        }
    }

    public static class StringUtils {
        public static string WithParDepth(this string s, int limit = int.MaxValue) {
            var sb = new StringBuilder(s.Length);
            int d = 0;
            foreach (var c in s) {
                if (c == '(') {
                    if (d < limit) {
                        sb.Append(c);
                        if (d < limit - 2) {
                            sb.Append(d);
                            sb.Append('#');
                        }
                    } else {
                        sb.Append('.');
                    }
                    d++;
                } else if (c == ')') {
                    --d;
                    if (d < limit) {
                        if (d < limit - 2) {
                            sb.Append('#');
                            sb.Append(d);
                        }
                        sb.Append(c);
                    } else {
                        sb.Append('.');
                    }
                } else {
                    if (d < limit) {
                        sb.Append(c);
                    } else {
                        sb.Append('.');
                    }
                }
            }
            return sb.ToString();
        }

        public static string WithParIndent(this string s, string indent = "  ") {
            var sb = new StringBuilder(s.Length);
            int d = 0;
            foreach (var c in s) {
                if (c == '(') {
                    if (d++ > 2) {
                        sb.AppendLine("" + c);
                        indent += "  ";
                        sb.Append(indent);
                    } else {
                        sb.Append(c);
                    }
                } else if (c == ')') {
                    if (--d > 2) {
                        indent = indent.Substring(2);
                        sb.AppendLine();
                        sb.Append(indent);
                        sb.Append(c);
                    } else {
                        sb.Append(c);
                    }
                } else {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}

namespace JetBrains.Annotations {
    /// <summary>
    /// Indicates that the value of marked element could be <c>null</c> sometimes, so the check for <c>null</c> is necessary before its usage
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class CanBeNullAttribute : Attribute {
    }

    /// <summary>
    /// Indicates that the value of marked element could never be <c>null</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute {
    }


}