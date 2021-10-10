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
        public PlayerLogic player;
        public List<MonsterLogic> playerMonsters = new List<MonsterLogic>();
        public List<MonsterLogic> enemyMonsters = new List<MonsterLogic>();
        public Dictionary<MonsterLogic, BattleTurnData> monsterBattleTurnDatas = new Dictionary<MonsterLogic, BattleTurnData>();
        public GameObject environment;
        public bool playerTurn = false;
        public bool alreadyPlayerMonsterHovered = false;
        public GameObject pointerClickedMonster = null;
        public List<GameObject> selectMonsters;

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