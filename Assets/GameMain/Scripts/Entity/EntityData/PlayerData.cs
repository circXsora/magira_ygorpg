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
    public class PlayerData : EntityData, ICloneable
    {
        public MonsterData[] MonsterDatas { get; set; }

        public PlayerData(int entityId, int typeId)
            : base(entityId, typeId)
        {
            IDataTable<DRPlayer> dtPlayer = GameEntry.DataTable.GetDataTable<DRPlayer>();
            DRPlayer drPlayer = dtPlayer.GetDataRow(TypeId);
            if (drPlayer == null)
            {
                throw new DataRowIsNullException("Player", TypeId);
            }
            Speed = drPlayer.Speed;
        }

        public float Speed
        {
            get;
            private set;
        }

        public object Clone()
        {
            var newData = new PlayerData(GameEntry.Entity.GenerateSerialId(), TypeId)
            {
                MonsterDatas = this.MonsterDatas
            };
            return newData;
        }
    }
}
