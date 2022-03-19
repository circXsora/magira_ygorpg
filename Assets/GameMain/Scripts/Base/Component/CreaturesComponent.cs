//------------------------------------------------------------------------------
//  <copyright file="CreaturesComponent.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/15 17:57:05
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using MGO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class MonsterState
    {
        public IntRange PhysicalAttackRange { get; set; }
        public int PhysicalResistance { get; set; }
        public int PhysicalCritical { get; set; }
        public float PhysicalCriticalRatio { get; set; }

        public IntRange MagicalAttackRange { get; set; }
        public int MagicalResistance { get; set; }
        public int MagicalCritical { get; set; }
        public float MagicalCriticalRatio { get; set; }

        public int Avoidance { get; set; }
    }

    public class MonsterEntry
    {
        public int? Star { get; set; }
        public int? Rank { get; set; }
    }

    /// <summary>
    /// 运行时会变化的信息
    /// </summary>
    public class CreatureState
    {
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
    }

    /// <summary>
    /// 具体的介绍和展示信息
    /// </summary>
    public class CreatureEntry
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
    }

    /// <summary>
    /// 初始化信息，Id号等信息
    /// </summary>
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

    public class CreaturesComponent : GameFrameworkComponent
    {
        private static int idGenerator = 0;
        private readonly Dictionary<int, CreatureEntity> creatures = new();
        private readonly CreatureFactory factory = new();

        [SerializeField]
        private CreatureVisualEffectConfigSO visualEffectConfig;

        [SerializeField]
        private GameObject playerTemplate;
        public GameObject PlayerTemplate => playerTemplate;

        [SerializeField]
        private GameObject enemyMonsterTemplate;
        public GameObject EnemyMonsterTemplate => enemyMonsterTemplate;

        [SerializeField]
        private GameObject playerMonsterTemplate;
        public GameObject PlayerMonsterTemplate => playerMonsterTemplate;

        [SerializeField]
        private GameObject monsterUITemplate;
        public GameObject MonsterUITemplate => monsterUITemplate;

        public CreatureEntity CreateCreature(CreatureInfo creatureInfo)
        {
            return factory.CreateEntity(creatureInfo);
        }

        public CreatureVisualEffectConfigSO GetCreatureVisualEffectConfig(int entryId)
        {
            return visualEffectConfig;
        }
    }
}