/****************************************************
 *  Copyright © 2021 circXsora. All rights reserved.
 *------------------------------------------------------------------------
 *  作者:  circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期:  2021/5/4 13:38:15
 *  项目:  BBYGO
 *  功能:
*****************************************************/

using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public sealed class PlayerTouchedMonsterEventArgs : GameEventArgs
	{
        public static readonly int EventId = typeof(PlayerTouchedMonsterEventArgs).GetHashCode();
        public RoomData roomData;
        public override int Id => EventId;

        /// <summary>
        /// 你不应该调用构造函数而应该调用Create静态方法
        /// </summary>
        public PlayerTouchedMonsterEventArgs()
        {

        }

        public static PlayerTouchedMonsterEventArgs Create(RoomData roomData)
        {
            PlayerTouchedMonsterEventArgs eventArgs = ReferencePool.Acquire<PlayerTouchedMonsterEventArgs>();
            eventArgs.roomData = roomData;
            return eventArgs;
        }

        public override void Clear()
        {

        }
	}
}