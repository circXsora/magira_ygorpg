//------------------------------------------------------------------------------
//  <copyright file="GameStartButtonClickedEventArgs.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/7 15:14:05
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public sealed class GameStartButtonClickedEventArgs : GameEventArgs
	{
        public static readonly int EventId = typeof(GameStartButtonClickedEventArgs).GetHashCode();
        
        public override int Id => EventId;

        /// <summary>
        /// 你不应该调用构造函数而应该调用Create静态方法
        /// </summary>
        public GameStartButtonClickedEventArgs()
        {

        }

        public static GameStartButtonClickedEventArgs Create(/*在这里加入构造参数*/)
        {
            GameStartButtonClickedEventArgs eventArgs = ReferencePool.Acquire<GameStartButtonClickedEventArgs>();
            return eventArgs;
        }

        public override void Clear()
        {

        }
	}
}