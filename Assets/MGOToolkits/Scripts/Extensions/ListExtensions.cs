using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            ListUtility.Shuffle(list);
        }
    }
}