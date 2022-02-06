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
    public class BattleContext : MonoBehaviour
    {

        public class BattleTurnData
        {
            public bool actionDone = false;
        }

        public bool InBattle { get; private set; }
        public PlayerEntity player;
        public List<MonsterEntity> playerMonsters = new List<MonsterEntity>();
        public List<MonsterEntity> enemyMonsters = new List<MonsterEntity>();
        public Dictionary<MonsterEntity, BattleTurnData> monsterBattleTurnDatas = new Dictionary<MonsterEntity, BattleTurnData>();
        public EnvironmentEntity environment;
        public bool playerTurn = false;
        public bool alreadyPlayerMonsterHovered = false;
        public MonsterEntity pointerClickedMonster = null;
        public List<MonsterEntity> selectMonsters;

        public void Init()
        {
            ClearCore();
            InBattle = true;
        }

        public void Clear()
        {
            ClearCore();
            InBattle = false;
        }

        private void ClearCore()
        {
            player = null;
            playerMonsters.Clear();
            enemyMonsters.Clear();
            environment = null;
            alreadyPlayerMonsterHovered = false;
            pointerClickedMonster = null;
        }
    }
}