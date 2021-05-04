using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO
{
    public static class FloatUtility
    {
        /// <summary>
        /// 将秒转换为毫秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int ToMill(this float time)
        {
            return (int)(time * 1000f);
        }
    }
}