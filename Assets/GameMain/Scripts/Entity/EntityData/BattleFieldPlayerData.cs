//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using System;
using UnityEngine;

namespace BBYGO
{
    [Serializable]
    public class BattleFieldPlayerData : EntityData
    {
        public MonsterData[] MonsterDatas { get; set; }
        public string PointName { get; set; }
        public int OwnerId { get; }

        public BattleFieldPlayerData(int entityId, int typeId, int ownerId)
            : base(entityId, typeId)
        {
            OwnerId = ownerId;
        }
    }
}
