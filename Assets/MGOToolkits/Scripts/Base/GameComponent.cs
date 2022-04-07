//------------------------------------------------------------------------------
//  <copyright file="GameComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/19 20:11:32
//  项目:  邦邦游戏王
//  功能:  顶层管理类，一切功能的入口
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGO
{
	public abstract class GameComponent : MonoBehaviour
	{
        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected virtual void Awake()
        {
            GameEntry.RegisterComponent(this);
        }
    }
}