using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfo : MonoBehaviour
{

    public class AttackInfo
    {
        public MonsterController AttackMonster;
        public MonsterController GetDamageMonster;
    }

    public class SkillProcessInfo
    {
        public MonsterController ActiveMonster;
        public List<MonsterController> FunctionOnMonsters = new List<MonsterController>();
        public Skill Skill;
    }
}
