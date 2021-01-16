using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster/New Monster")]
public class MonsterInfo : ScriptableObject
{
    public string MonsterName;
    public Sprite Sprite;
    public int MaxLevel;
    public int Rank;
    public string Intrduction;
    public List<LevelData> LevelDatas;

    [System.Serializable]
    public class LevelData
    {
        public bool Keep = true;
        public SpecialSkill[] SpecialSkills;
        public BattleSKill[] BattleSkills;

        public float MaxHealth;
        public int MaxBattleSkillPoint;

        public float AttackValue;
        public float DefenseValue;
    }

}
