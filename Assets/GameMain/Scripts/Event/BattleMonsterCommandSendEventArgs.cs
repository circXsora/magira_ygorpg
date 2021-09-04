//------------------------------------------------------------------------------
//  <copyright file="MonsterBattleCommandEventArgs.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/30 22:37:22
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
	public sealed class BattleMonsterCommandSendEventArgs : GameEventArgs
	{
        public static readonly int EventId = typeof(BattleMonsterCommandSendEventArgs).GetHashCode();
        
        public override int Id => EventId;
        public CreatureView view;
        public BattleMonsterCommandSO command;
        /// <summary>
        /// 你不应该调用构造函数而应该调用Create静态方法
        /// </summary>
        public BattleMonsterCommandSendEventArgs()
        {

        }

        public static BattleMonsterCommandSendEventArgs Create(CreatureView view, BattleMonsterCommandSO command)
        {
            BattleMonsterCommandSendEventArgs eventArgs = ReferencePool.Acquire<BattleMonsterCommandSendEventArgs>();
            eventArgs.view = view;
            eventArgs.command = command;
            return eventArgs;
        }

        public override void Clear()
        {

        }
	}
}