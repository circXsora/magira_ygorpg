//------------------------------------------------------------------------------
//  <copyright file="BattleContextSO.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/11 23:29:15
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace BBYGO
{
	public class BattleContext
	{
		public PlayerLogic player;
		public List<MonsterLogic> playerMonsters = new List<MonsterLogic>();
		public List<MonsterLogic> enemyMonsters = new List<MonsterLogic>();
		public GameObject environment;

        public void Init()
        {
			player = null;
			playerMonsters.Clear();
			enemyMonsters.Clear();
			environment = null;
        }
    }
}