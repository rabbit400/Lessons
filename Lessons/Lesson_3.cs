﻿using System;
using System.Collections.Generic;

namespace Lessons
{
    /// This to introduce/recall 
    ///  - the Map function (called Select in linq)
    ///  - start thinking about when and where 
    ///    memory is allocated 
    ///    
    /// Also to hint on when the source data is actually read.
    /// I.e. when a change to the source data will affect the
    /// result and when not.
    public static class Lesson_3
    {
        public static void Read_me()
        {
            // Say we have a list of floats thats supposed to be
            // percentages between 0 and 1
            // We want to clean them up to: ints from 0 to 100.
            IEnumerable<float> floats = new float[]
            {
            -0.2f, 0.55f, 3456346457.768467f, 34.1f, 1f
            };
            IEnumerable<int> percentages_as_ints = null;

            // (a) We can do
            {
                List<int> ret = new List<int>();
                foreach (float f in floats)
                {
                    float clamped = f.Clamp(0f, 1f);
                    float percentage = 100f * clamped;
                    int rounded = (int)percentage;
                    ret.Add(rounded);
                }
                percentages_as_ints = (IEnumerable<int>)ret;
            }

            // (b) is shorter, clearer and avoids comming
            // up with names for "clamped", "percentage"
            // and "rounded".
            percentages_as_ints = floats
                .Map_ft(f => (int)(100f * f.Clamp(0f, 1f)));

            // you are meant to read Map_ft() now

            // (c) this is 
            // - often cleaner (sometimes not)
            // - naturally divided into many small parts
            percentages_as_ints = floats
                .Map_ft(f => f.Clamp(0f, 1f))
                .Map_ft(f => 100f * f)
                .Map_ft(f => (int)f);

            // Sadly (c) will create 3 List<> instances
            // that are all grown dynamically.
            // I.e. if our float-list is long each of these 
            // intermediate lists will re-allocate memory 
            // many times as it grows.

            // (d)
            //  - does not create any "in the middle" lists
            //  - still creates several enumerators so still
            //      a small amount of overhead.
            percentages_as_ints = floats
                .Map(f => f.Clamp(0f, 1f))
                .Map(f => 100f * f)
                .Map(f => (int)f);

            // read Map() now if you didnt already

            // This Map(...) is the same as Select(...) in linq.
            // Every language except c# call it Map:
            // https://en.wikipedia.org/wiki/Map_(higher-order_function)#Language_comparison

            // To discuss with flat-mates:
            //  - Wait what! Map _never_ creates a list?
            //  - Where is cleaned_floats created then?
            //  - What happens if change the values in floats?
            //  - Have everyone done their XBX?
        }

        public static float Clamp(this float f, float min, float max)
            => Math.Max(min, Math.Min(max, f));

        // First try to write Map.
        // R for result, A for argument, _ft for first try
        public static IEnumerable<R> Map_ft<A, R>(this IEnumerable<A> list, Func<A, R> transform)
        {
            List<R> ret = new List<R>();
            foreach (var e in list)
            {
                R transformed_element = transform(e);
                ret.Add(transformed_element);
            }
            return (IEnumerable<R>)ret;
        }

        // Better Map
        public static IEnumerable<R> Map<A, R>(this IEnumerable<A> list, Func<A, R> transform)
        {
            foreach (A a in list)
            {
                R transformed_element = transform(a);
                yield return transformed_element;
            }
        }


        public struct Vector3 { public float x; public float y; public float z; }
        public struct Float3 { public float x; public float y; public float z; }

        public static IEnumerable<Float3> Exercise_1()
        {
            IEnumerable<Vector3> vectors = new Vector3[]
            {
                new Vector3{x = 1f, y = 2f, x = 3f},
                new Vector3{x = 2f, y = -1f, x = -3f},
            };
            // You have a list of points of type Vector3.
            // Convert them to a list of Float3.
            // But for some silly reason the Vector3 points have 
            // y as height coordinate and z as forward.
            // Fix this so that the Float3 points are proper 
            // with y as forward and z as up.
            IEnumerable<Float3> floats = null;

            return floats;
        }

        public struct Person
        {
            public string FullName;
            public int Id;
        }
        public struct BothNames
        { 
            public string FirstName;
            public string LastName;
        }
        public static BothNames GetBothNames(string fullname)
        {
            string[] allNames = fullname.Split(" ");
            if (allNames.Length != 2)
                throw new NotImplementedException("Whopsi");

            return new BothNames
            {
                FirstName = allNames[0],
                LastName = allNames[1]
            };
        }

        public static void Excercise_2(
            out Dictionary<int, string> IdToFirstName,
            out Dictionary<int, string> IdToLastName)
        {
            IEnumerable<Person> persons = new Person[]
            {
                new Person { FullName = "Cute Mouse", Id = 13 },
                new Person { FullName =  "Cute Cat", Id = 31 },
            };
            // Create the proper dictionaries.
            // Id use GetBothNames, Select/Map, and linqs ToDictionary.
            IdToFirstName = null;
            IdToLastName = null;
        }
    }
}
