//------------------------------------------------------------------------------
//  <copyright file="BattleCreatureBeSelectEventArgs.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/9/1 22:01:16
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
	public sealed class CreatureViewBeEnteredEventArgs : GameEventArgs
	{
        public static readonly int EventId = typeof(CreatureViewBeEnteredEventArgs).GetHashCode();
        
        public override int Id => EventId;

        public PointerEventData data;

        /// <summary>
        /// 你不应该调用构造函数而应该调用Create静态方法
        /// </summary>
        public CreatureViewBeEnteredEventArgs()
        {

        }

        public static CreatureViewBeEnteredEventArgs Create(PointerEventData data)
        {
            CreatureViewBeEnteredEventArgs eventArgs = ReferencePool.Acquire<CreatureViewBeEnteredEventArgs>();
            eventArgs.data = data;
            return eventArgs;
        }

        public override void Clear()
        {

        }
	}
}