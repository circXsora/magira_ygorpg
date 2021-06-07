/****************************************************
 *  Copyright © 2021 circXsora. All rights reserved.
 *------------------------------------------------------------------------
 *  作者:  circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期:  2021/5/4 13:41:22
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
	public sealed class PlayerArrivedRoomEventArgs : GameEventArgs
	{
        public static readonly int EventId = typeof(PlayerArrivedRoomEventArgs).GetHashCode();
        
        public override int Id => EventId;

        public RoomLogic Room;

        /// <summary>
        /// 你不应该调用构造函数而应该调用Create静态方法
        /// </summary>
        public PlayerArrivedRoomEventArgs()
        {

        }

        public static PlayerArrivedRoomEventArgs Create(RoomLogic room)
        {
            PlayerArrivedRoomEventArgs eventArgs = ReferencePool.Acquire<PlayerArrivedRoomEventArgs>();
            eventArgs.Room = room;
            return eventArgs;
        }

        public override void Clear()
        {

        }
	}
}