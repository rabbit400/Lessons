using System;
using System.Collections.Generic;

namespace Lessons
{
    public static class Lesson_1_answers
    {
        public static Dictionary<string, bool> ToDictionary_a(
            this IEnumerable<int> source,
            Func<int, string> keySelector,
            Func<int, bool> valueSelector)
        {
            Dictionary<string, bool> ret = new Dictionary<string, bool>();
            foreach (int source_element in source)
            {
                string key = keySelector(source_element);
                bool value = valueSelector(source_element);
                ret.Add(key, value);
            }
            return ret;
        }

        public static Dictionary<K, bool> ToDictionary_b<K>(
            this IEnumerable<int> source,
            Func<int, K> keySelector,
            Func<int, bool> valueSelector)
        {
            Dictionary<K, bool> ret = new Dictionary<K, bool>();
            foreach (int source_element in source)
            {
                K key = keySelector(source_element);
                bool value = valueSelector(source_element);
                ret.Add(key, value);
            }
            return ret;
        }

        public static Dictionary<K, V> ToDictionary_c<K, V>(
            this IEnumerable<int> source,
            Func<int, K> keySelector,
            Func<int, V> valueSelector)
        {
            Dictionary<K, V> ret = new Dictionary<K, V>();
            foreach (int source_element in source)
            {
                K key = keySelector(source_element);
                V value = valueSelector(source_element);
                ret.Add(key, value);
            }
            return ret;
        }

        public static Dictionary<K, V> ToDictionary_d<S, K, V>(
            this IEnumerable<S> source,
            Func<S, K> keySelector,
            Func<S, V> valueSelector)
        {
            Dictionary<K, V> ret = new Dictionary<K, V>();
            foreach (S source_element in source)
            {
                K key = keySelector(source_element);
                V value = valueSelector(source_element);
                ret.Add(key, value);
            }
            return ret;
        }
    }
}
