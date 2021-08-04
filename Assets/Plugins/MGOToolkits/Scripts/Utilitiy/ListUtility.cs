using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGO
{
    public static class ListUtility
    {
        public static void Shuffle<T>(IList<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int index = Random.Range(i, list.Count);
                T value = list[index];
                list[index] = list[i];
                list[i] = value;
            }
        }
    }
}