using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace MGO
{
    public static class ArrayExtension
    {
        public static TSource Random1<TSource>(this IEnumerable<TSource> source)
        {
            var r = new System.Random();
            if (source.Count() <= 0)
            {
                throw new System.Exception("The Array can't be empty");
            }
            var i = r.Next(0, source.Count());
            return source.ElementAt(i);
        }
    }
}