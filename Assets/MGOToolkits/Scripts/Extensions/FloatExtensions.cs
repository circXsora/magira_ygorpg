//------------------------------------------------------------------------------
//  <copyright file="FloatExtensions.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/10/23 15:43:53
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FloatExtensions
{
    public static int ToMillisecond(this float value)
    {
        return (int)(value * 1000);
    }
}
