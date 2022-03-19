//------------------------------------------------------------------------------
//  <copyright file="GameObjectExtensions.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/19 20:45:55
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using UnityEngine;

namespace MGO
{
    public static class GameObjectExtensions
	{
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
			var component = gameObject.GetComponent<T>();
			if (component == null)
            {
				component = gameObject.AddComponent<T>();
            }
			return component;
		}
	}
}