//------------------------------------------------------------------------------
//  <copyright file="BattleFormParams.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/16 16:50:01
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBYGO
{
	public class BattleFormParams
	{
		public MonsterData[] PlayerMonsterDatas { get; set; }

        public BattleFormParams(MonsterData[] playerMonsterDatas)
        {
			PlayerMonsterDatas = playerMonsterDatas;
		}
	}
}