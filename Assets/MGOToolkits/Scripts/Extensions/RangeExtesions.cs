//------------------------------------------------------------------------------
//  <copyright file="RangeExtesions.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/10 21:28:33
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO
{
    public static class RangeExtesions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intRange"></param>
        /// <param name="leftOpen">左边是否是开区间</param>
        /// <param name="rightOpen">右边是否是开区间</param>
        /// <returns></returns>
        public static int Random(this IntRange intRange, bool leftOpen = false, bool rightOpen = false)
        {
            return UnityEngine.Random.Range(leftOpen ? intRange.Min + 1 : intRange.Min, rightOpen ? intRange.Max + 1 : intRange.Max);
        }
    }
}