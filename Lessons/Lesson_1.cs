using System;
using System.Collections.Generic;
using System.Linq;

namespace Lessons
{
    /// Hallo!
    /// For lesson 1 you will do 4 simple exercises.
    /// Answers are last.
    /// Peek if you need to but make sure you understand them.
    public static class Lesson_1_Exercise_a
    {
        // Implement this!
        public static Dictionary<string, bool> ToDictionary(
            this IEnumerable<int> source,
            Func<int, string> keySelector,
            Func<int, bool> valueSelector)
        {
            throw new NotImplementedException();
        }

        public static void Test()
        {
            int[] someInts = new int[] { 1, 2, 3, 17, 34 };

            Dictionary<string, bool> is_odd_lookup_1
                = someInts.ToDictionary(
                    keySelector: i => $"{i}",
                    valueSelector: i => i % 2 == 1
                    );

            Dictionary<string, bool> is_odd_lookup_2
                = someInts.ToDictionary(GetKey, IsOdd);

            // Note how we used both "function pointers"
            // and lambdas for key and value selectors.
            // Either of those works as a Func<>.
            // (of course also delegates)

            Lesson_1_testing.Ensure_correct(is_odd_lookup_1);
            Lesson_1_testing.Ensure_correct(is_odd_lookup_2);
        }

        public static bool IsOdd(int i) => i % 2 == 1;
        public static string GetKey(int i) => $"{i}";
    }

    public static class Lesson_1_Exercise_b
    {
        // Its silly to use string keys for integers.
        // Write this for generic keys instead.
        public static Dictionary<K, bool> ToDictionary<K>(
            this IEnumerable<int> source,
            Func<int, K> keySelector,
            Func<int, bool> valueSelector)
        {
            throw new NotImplementedException();
        }

        public static void Test()
        {
            int[] someInts = new int[] { 1, 2, 3, 17, 34 };

            Dictionary<long, bool> is_odd_lookup_2
                = someInts.ToDictionary(GetKey, IsOdd);

            Dictionary<int, bool> is_odd_lookup_1
                = someInts.ToDictionary(i => i, i => i % 2 == 1);

            Lesson_1_testing.Ensure_correct(is_odd_lookup_1);
            Lesson_1_testing.Ensure_correct(is_odd_lookup_2);

            // note how the key types are different for illustration - A* pedagog!
        }

        public static bool IsOdd(int i) => i % 2 == 1;
        public static long GetKey(int i) => (long)i;
    }

    public static class Lesson_1_Exercise_c
    {
        // Write a ToDictionary that returns a Dictionary<K, V>.
        // I.e. the value type is generic or "free" (not hardcoded to byte).

        // For tests you should copy Test() from exercise b.
        // I dont put it here to save space.
    }

    public static class Lesson_1_Exercise_d
    {
        // Write a ToDictionary where every type is generic or "free".
        // I.e. the source argument should be IEnumerable<S>
    }

    // GOOD JOB!
    // You likely implemented:
    // public static Dictionary<K, V> ToDictionary_d<S, K, V>(
    //     this IEnumerable<S> source,
    //     Func<int, K> keySelector,
    //     Func<int, V> valueSelector)
    // which is pretty much what linq does.
    //
    // Though they call it:
    // public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
    //     this IEnumerable<TSource> source,
    //     Func<TSource, TKey> keySelector,
    //     Func<TSource, TElement> elementSelector);


    /// <summary>
    /// This is just to check your solution.
    /// Not part of the lesson to read these.
    /// </summary>
    public static class Lesson_1_testing
    {
        public static bool IsOdd(int i) => i % 2 == 1;

        public static void Enforce_true(this bool value)
        {
            if (!value) throw new Exception("FAILED");
        }

        public static bool Is_correct(int integer_value, bool is_odd)
            => is_odd == IsOdd(integer_value);


        public static void Enforce_correct<K>(Dictionary<K, bool> int_to_odd, Func<K, int> get_int)
            => int_to_odd
            .All(kvp => Is_correct(get_int(kvp.Key), kvp.Value))
            .Enforce_true();


        public static void Ensure_correct(Dictionary<string, bool> int_to_odd)
            => Enforce_correct(int_to_odd, s => int.Parse(s));
        public static void Ensure_correct(Dictionary<int, bool> int_to_odd)
            => Enforce_correct(int_to_odd, i => i);
        public static void Ensure_correct(Dictionary<long, bool> int_to_odd)
            => Enforce_correct(int_to_odd, l => (int)l);
    }
}
