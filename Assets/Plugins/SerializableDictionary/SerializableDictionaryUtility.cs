using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SerializableDictionaryUtility
{
    public static void GenerateEnumKeysData<TKey, TValue>(this SerializableDictionary<TKey, TValue> datas) where TKey : System.Enum where TValue : class
    {

#if UNITY_EDITOR

        if (datas == null)
        {
            datas = new SerializableDictionary<TKey, TValue>();
        }

        foreach (TKey item in System.Enum.GetValues(typeof(TKey)))
        {
            if (!datas.ContainsKey(item))
            {
                datas.Add(item, null);
            }
        }
#endif

    }
}