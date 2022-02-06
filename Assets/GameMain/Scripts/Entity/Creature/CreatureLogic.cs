//------------------------------------------------------------------------------
//  <copyright file="CreatureEntity.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/11/21 15:40:19
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO.Entity.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public class CreatureLogic : EntityLogic
	{
        public CreatureInfo Info { get; private set; }
        public virtual CreatureAI AI { get; set; }
        public CreatureState CreatureState { get; set; }
        public CreatureEntry EntryData { get; set; }

        public CreatureLogic(CreatureInfo info)
        {
            Info = info;
            CreatureState = new CreatureState() { Hp = 50, MaxHp = 50 };
        }
    }
}