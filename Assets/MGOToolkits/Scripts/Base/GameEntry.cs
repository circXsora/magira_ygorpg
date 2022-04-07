//------------------------------------------------------------------------------
//  <copyright file="GameEntry.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2022/3/19 20:13:07
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MGO
{

    /// <summary>
    /// 关闭游戏框架类型。
    /// </summary>
    public enum ShutdownType : byte
    {
        /// <summary>
        /// 仅关闭游戏框架。
        /// </summary>
        None = 0,

        /// <summary>
        /// 关闭游戏框架并重启游戏。
        /// </summary>
        Restart,

        /// <summary>
        /// 关闭游戏框架并退出游戏。
        /// </summary>
        Quit,
    }

    /// <summary>
    /// 游戏入口。
    /// </summary>
    public static class GameEntry
    {
        private static readonly LinkedList<GameComponent> s_GameFrameworkComponents = new LinkedList<GameComponent>();

        /// <summary>
        /// 注册游戏框架组件。
        /// </summary>
        /// <param name="gameFrameworkComponent">要注册的游戏框架组件。</param>
        internal static void RegisterComponent(GameComponent gameFrameworkComponent)
        {
            s_GameFrameworkComponents.AddLast(gameFrameworkComponent);
        }
    }
}