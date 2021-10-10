//------------------------------------------------------------------------------
//  <copyright file="CreaturesComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/15 17:57:05
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class CreatureInfo
    {
        public int id;
        public int entryId;
        public CreaturesType type;
        public CreaturesParty party;
    }

    public enum CreaturesType
    {
        Monsters, 
        Player
    }

    public enum CreaturesParty
    {
        Player,
        Enemy,
    }

	public class CreaturesComponent : UnityGameFramework.Runtime.GameFrameworkComponent
	{
        private static int idGenerator = 0;
        private readonly Dictionary<int, CreatureLogic> creaturesDic = new Dictionary<int, CreatureLogic>();
        private readonly CreatureFactory factory = new CreatureFactory();
        private EventSO LoadCreatureCompelteEvent;

        [SerializeField]
        private GameObject playerTemplate;

        public GameObject PlayerTemplate => playerTemplate;

        [SerializeField]
        private GameObject enemyMonsterTemplate;

        public CreatureLogic GetCreatureLogicByGameObjerct(GameObject pointerClickedMonster)
        {
            var view = pointerClickedMonster.GetComponent<CreatureView>();
            var creature = creaturesDic.Values.First(c => c.View == view);
            return creature;
        }

        public GameObject EnemyMonsterTemplate => enemyMonsterTemplate;

        [SerializeField]
        private GameObject playerMonsterTemplate;

        public GameObject PlayerMonsterTemplate => playerMonsterTemplate;

        private CreatureLogic CreateLogic(CreatureInfo info)
        {
            var logic = factory.CreateLogic(info);
            creaturesDic.Add(logic.Info.id, logic);
            return logic;
        }

        private CreatureView CreateView(CreatureInfo info)
        {
            var view = factory.CreateView(info);
            return view;
        }


        public CreatureLogic Load(CreatureInfo info)
        {
            info.id = idGenerator++;
            var logic = CreateLogic(info);
            var view = CreateView(info);
            logic.SetView(view);
            return logic;
        }

        public void DestroyCreature(int creatureId)
        {
            if (creaturesDic.TryGetValue(creatureId, out var creatureLogic))
            {
                creaturesDic.Remove(creatureId);
                creatureLogic.DestroyView();
            }
        }
    }
}