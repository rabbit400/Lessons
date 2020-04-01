namespace Lessons
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // TODO writing Take should be an exercise

    public static class Lesson_2_answers
    {
        public static class Exercise_1
        {
            public static IEnumerable<T> Take_answer<T>
            (this IEnumerable<T> elements, int num_elements_to_take)
            {
                int i = 0; // number of elements taken so far
                foreach (T element in elements)
                {
                    if (i >= num_elements_to_take)
                        yield break;
                    yield return element;
                    i++;
                }
            }

            /*Examples of wrong answers with explanations.
             *
             * This is wrong because:
             * 1) It doesnt handle when there are fewer
             *       than num_elements_to_take.
             * 2) There is no [] operator on IEnumerable.
            public static IEnumerable<T> Take<T>
                (this IEnumerable<T> source, int num_elements_to_take)
            {
                for (int i = 0; i < num_elements_to_take; i++)
                    yield return elements[i];
            }
            * 
            * An IEnumerable is conceptually something where you can
            * only ask for the next element. Unlike an array you cant
            * ask lenght or element at index. (At least not in a the
            * simple and performant way you can with an array.)
            * 
            * If you want to use a normal for loop youll have to get
            * an IEnumerator from the elements to access the 
            * "has next element" and "get next element" questions.
            */
            public static IEnumerable<T> Take_answer_2<T>
                (this IEnumerable<T> elements, int num_elements_to_take)
            {
                IEnumerator<T> enumerator = elements.GetEnumerator();
                for (int i = 0; i < num_elements_to_take; i++)
                {
                    if (enumerator.MoveNext())
                        yield return enumerator.Current;
                    else
                        break; // no more elements
                }
            }
            // Or shorter
            public static IEnumerable<T> Take_answer_3<T>
                (this IEnumerable<T> elements, int num_elements_to_take)
            {
                IEnumerator<T> enumerator = elements.GetEnumerator();
                for (int i = 0; i < num_elements_to_take && enumerator.MoveNext(); i++)
                    yield return enumerator.Current;
            }
            // But I prefer the foreach in most cases.
            // Usually simpler to read and understand.
        }

        public static class Exercise_2
        {
            // The natural answer.
            public static T AtIndex_answer<T>
                (IEnumerable<T> elements, int index)
            {
                int i = 0;
                foreach (T element in elements)
                {
                    if (i == index) return element;
                    i++;
                }
                // elements is to short for index
                throw new IndexOutOfRangeException();
            }

            // Using exisisting tools.
            public static T AtIndex_answer_2<T>
                (IEnumerable<T> elements, int index)
            {
                IEnumerable<T> elements_starting_at_index
                    = elements.Skip(index);
                foreach (T element in elements_starting_at_index)
                    return element;
                throw new IndexOutOfRangeException();
            }

            // It is a reasonable idea to not throw an exception
            // and instead return defult(T) when the index is bad.
            // Examples here are named like linq functions and 
            // implemented with linq functions:
            public static T ValueAtIndex<T>
                (IEnumerable<T> elements, int index)
                => elements
                .Skip(index)
                .First(); // this throws if no elements left
            public static T ValueOrDefaultAtIndex<T>
                (IEnumerable<T> elements, int index)
                => elements
                .Skip(index)
                .FirstOrDefault(); // this returns default if no elements left
        }
    }
}