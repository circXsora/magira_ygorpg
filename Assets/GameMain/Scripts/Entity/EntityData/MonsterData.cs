/****************************************************
 *  Copyright © 2021 circXsora. All rights reserved.
 *------------------------------------------------------------------------
 *  作者:  circXsora
 *  邮箱:  circXsora@outlook.com
 *  日期:  2021/5/4 13:17:55
 *  项目:  BBYGO
 *  功能:
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class MonsterData : EntityData
    {
        public int Level;
        public MonsterData(int typeId, int level = 1) : base(GameEntry.Entity.GenerateSerialId(), typeId)
        {
            Level = level;
        }
    }
}