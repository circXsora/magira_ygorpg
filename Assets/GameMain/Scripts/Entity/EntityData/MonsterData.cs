/****************************************************
 *  Copyright © 2021 circXsora. All rights reserved.
 *------------------------------------------------------------------------
 *  作者:  circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期:  2021/5/4 13:17:55
 *  项目:  BBYGO
 *  功能:
*****************************************************/

using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class MonsterData : EntityData
    {
        public DRMonster EntryData { get; set; }
        public int CurrentHealthValue { get; set; }
        public int CurrentMagicValue { get; set; }
        public int CurrentLevel { get; set; }
        public MonsterData(int typeId, int level = 1) : base(GameEntry.Entity.GenerateSerialId(), typeId)
        {
            EntryData = GameEntry.DataTable.GetDataTable<DRMonster>().GetDataRow(typeId);
            CurrentLevel = level;
            var levelTableData = GameEntry.DataTable.GetDataTable<DRMonsterLevel>();
            var levelRowData = levelTableData.GetDataRow(typeId * 1000 + level);
            CurrentHealthValue = levelRowData.MaxHealth;
            CurrentMagicValue = levelRowData.MaxMagic;
        }
    }
}