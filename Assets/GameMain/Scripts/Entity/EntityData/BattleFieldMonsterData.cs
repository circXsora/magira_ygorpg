//------------------------------------------------------------------------------
//  <copyright file="BattleFieldMonsterData.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/6/6 21:45:25
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------


using BBYGO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
    public class BattleFieldMonsterData : EntityData
    {
        public int OwnerId { get; set; }
        public string PointName { get; set; }

        public BattleFieldMonsterData(int entityId, int typeId) : base(entityId, typeId)
        {

        }
    }
}