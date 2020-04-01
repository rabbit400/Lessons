namespace Lessons
{
    using System.Collections.Generic;

    // TODO writing Take should be an exercise

    public static class Lesson_2_answers
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
    }
}