using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGO
{
    public static class ArrayExtension
    {
        public static TSource Random1<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null || source.Count() <= 0)
            {
                throw new System.Exception("The Array can't be empty");
            }
            var i = Random.Range(0, source.Count());
            return source.ElementAt(i);
        }
    }
}