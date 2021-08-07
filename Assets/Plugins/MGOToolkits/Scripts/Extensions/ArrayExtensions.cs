using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGO
{
    public static class ArrayExtensions
    {
        public static TSource Random1<TSource>(this ICollection<TSource> source)
        {
            if (source == null || source.Count <= 0)
            {
                throw new System.Exception("The Array can't be empty");
            }
            var i = Random.Range(0, source.Count());
            return source.ElementAt(i);
        }
        public static object Random1(this ICollection source)
        {
            if (source == null || source.Count <= 0)
            {
                throw new System.Exception("The Array can't be empty");
            }
            var index = Random.Range(0, source.Count);
            var enumerator = source.GetEnumerator();
            for (int i = 0; i < index; i++)
            {
                enumerator.MoveNext();
            }
            return enumerator.Current;
        }
    }
}