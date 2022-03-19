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
        private static readonly LinkedList<GameFrameworkComponent> s_GameFrameworkComponents = new LinkedList<GameFrameworkComponent>();

        /// <summary>
        /// 游戏框架所在的场景编号。
        /// </summary>
        internal const int GameFrameworkSceneId = 0;

        /// <summary>
        /// 获取游戏框架组件。
        /// </summary>
        /// <typeparam name="T">要获取的游戏框架组件类型。</typeparam>
        /// <returns>要获取的游戏框架组件。</returns>
        public static T GetComponent<T>() where T : GameFrameworkComponent
        {
            return (T)GetComponent(typeof(T));
        }

        /// <summary>
        /// 获取游戏框架组件。
        /// </summary>
        /// <param name="type">要获取的游戏框架组件类型。</param>
        /// <returns>要获取的游戏框架组件。</returns>
        public static GameFrameworkComponent GetComponent(Type type)
        {
            return s_GameFrameworkComponents.First(c => c.GetType() == type);
        }

        /// <summary>
        /// 获取游戏框架组件。
        /// </summary>
        /// <param name="typeName">要获取的游戏框架组件类型名称。</param>
        /// <returns>要获取的游戏框架组件。</returns>
        public static GameFrameworkComponent GetComponent(string typeName)
        {
            return s_GameFrameworkComponents.First(c => c.GetType().FullName == typeName || c.GetType().FullName == typeName);
        }

        /// <summary>
        /// 关闭游戏框架。
        /// </summary>
        /// <param name="shutdownType">关闭游戏框架类型。</param>
        public static void Shutdown(ShutdownType shutdownType)
        {
//            Log.Info("Shutdown Game Framework ({0})...", shutdownType.ToString());
//            BaseComponent baseComponent = GetComponent<BaseComponent>();
//            if (baseComponent != null)
//            {
//                baseComponent.Shutdown();
//                baseComponent = null;
//            }

//            s_GameFrameworkComponents.Clear();

//            if (shutdownType == ShutdownType.None)
//            {
//                return;
//            }

//            if (shutdownType == ShutdownType.Restart)
//            {
//                SceneManager.LoadScene(GameFrameworkSceneId);
//                return;
//            }

//            if (shutdownType == ShutdownType.Quit)
//            {
//                Application.Quit();
//#if UNITY_EDITOR
//                UnityEditor.EditorApplication.isPlaying = false;
//#endif
//                return;
//            }
        }

        /// <summary>
        /// 注册游戏框架组件。
        /// </summary>
        /// <param name="gameFrameworkComponent">要注册的游戏框架组件。</param>
        internal static void RegisterComponent(GameFrameworkComponent gameFrameworkComponent)
        {
            if (gameFrameworkComponent == null)
            {
                Debug.LogError("Game Framework component is invalid.");
                return;
            }
            var instance = GetComponent(gameFrameworkComponent.GetType());
            if (instance != null)
            {
                Debug.LogError($"Game Framework component type '{gameFrameworkComponent.GetType().FullName}' is already exist.");
                return;
            }
            s_GameFrameworkComponents.AddLast(gameFrameworkComponent);
        }
    }
}