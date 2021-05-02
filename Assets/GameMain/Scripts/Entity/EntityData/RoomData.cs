using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public enum RoomType
    {
        None,
        Start,
        Normal,
        End,
    }

    public class RoomData : EntityData
    {
        public RoomType Type = RoomType.Normal;
        public int? StepFormOrigin;
        public RoomData[] WithRooms = new RoomData[4];
        public RoomData(int typeId = 20000) : base(GameEntry.Entity.GenerateSerialId(), typeId)
        {

        }
    }
}