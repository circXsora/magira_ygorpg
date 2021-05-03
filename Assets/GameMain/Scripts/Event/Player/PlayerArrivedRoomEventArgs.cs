using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class PlayerArrivedRoomEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(PlayerArrivedRoomEventArgs).GetHashCode();
        public Room Room;
        public PlayerArrivedRoomEventArgs Fill(Room room)
        {
            Room = room;
            return this;
        }
        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public override void Clear()
        {

        }
    }

}