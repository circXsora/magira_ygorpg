//------------------------------------------------------------------------------
//  <copyright file="IntRange.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/10 21:20:59
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO
{
    public class IntRange
    {
        public IntRange():this(0, 0)
        {
            
        }

        public IntRange(int min, int max)
        {
            SetRange(min, max);
        }

        public void SetRange(int? min, int? max)
        {
            if (min != null)
            {
                Min = min.Value;
            }
            if (max != null)
            {
                Max = max.Value;
            }
            Check();
        }

        private void Check()
        {
            if (Min > Max)
            {
                throw new System.ArgumentException("最小值不能比最大值更大。");
            }
        }
        public int Min { get; private set; }
        public int Max { get; private set; }
    }
}