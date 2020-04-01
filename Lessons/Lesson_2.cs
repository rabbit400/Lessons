namespace Lessons
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using static Lesson_2_functions;
    /// This is to introduce or recall
    ///  - enumerators
    ///  - concept of infinite lists
    ///  - using yield return
    ///  - using yield return to make infinite lists
    ///  - the Skip and Take functions
    public static class Lesson_2
    {
        public static void Read_me()
        {
            // Your task is to read and understand the loops below.
            // One at a time.

            All_numbers_enumerator enumerator = new All_numbers_enumerator();

            // this will print 1 2 3 ... and never stop
            while (enumerator.MoveNext())
                Console.Write(" " + enumerator.Current);

            // this will print 1 2 3 and stop
            foreach (int i in Number_1_to_3())
                Console.Write(" " + i);

            // this will print 0 1 2 ... and never stop
            foreach (int i in All_numbers())
                Console.Write(" " + i);

            // whops, 0 is not a number - lets fix that
            // this will print 1 2 3 ... and never stop
            foreach (int i in All_numbers().Skip(1))
                Console.Write(" " + i);

            // lets stop after 3 numbers
            // this will print 1 2 3 and stop
            foreach (int i in All_numbers().Skip(1).Take(3))
                Console.Write(" " + i);
        }
    }

    public static class Lesson_2_functions
    {
        public static IEnumerable<T> Skip<T>
            (this IEnumerable<T> elements, int num_elements_to_skip)
        {
            int i = 0; // number of elements skipped so far
            foreach (T element in elements)
            {
                if (i < num_elements_to_skip)
                    i++;
                else
                    yield return element;
            }
        }

        public static IEnumerable<T> Take<T>
            (this IEnumerable<T> elements, int num_elements_to_take)
        {
            // Exercise!
            // implement this
            throw new NotImplementedException();
        }

        public sealed class All_numbers_enumerator : IEnumerator<int>
        {
            private int current = 0;
            public int Current => current;
            public bool MoveNext()
            {
                current++;
                return true;
            }

            #region useless stuffs
            object IEnumerator.Current => current;
            public void Dispose() { } // do nothing
            public void Reset() => throw new NotImplementedException(); // I dont know what this is
            #endregion
        }

        // Sugar for writing an IEnumerator
        //   and turning it into IEnumerable
        // There are many similar variations of this sugar
        public static IEnumerable<int> Number_1_to_3()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        public static IEnumerable<int> All_numbers()
        {
            for (int i = 0; /* never stop */; i++)
                yield return i;
        }
    }
}