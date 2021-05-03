using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{

    public class WallData : EntityData
    {
        public int? OwnerID;
        public WallData(int typeId, int ownerId) : base(GameEntry.Entity.GenerateSerialId(), typeId)
        {
            OwnerID = ownerId;
        }
    }
}