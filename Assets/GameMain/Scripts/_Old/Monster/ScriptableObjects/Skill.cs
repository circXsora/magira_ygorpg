using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public abstract class Skill : ScriptableObject
{
    [Serializable]
    public enum SkillType
    {
        Direct, // 直接造成影响
        Status, // 持续造成影响
    }
    public SkillType Type;

    [Flags]
    [Serializable]
    public enum SkillRange : uint
    {
        Self = 0x01,
        Friends = 0x02,
        Enemies = 0x04,
    }
    public SkillRange Range;
    // 取对象还是不取对象
    public bool SelectingTarget = true;
    // 可以作用于多少只怪兽上
    public int MonsterCount = 1;

    [Serializable]
    public enum SkillEffect
    {
        Heal, // 回复
        DamageFactor, // 造成倍数伤害
        DamageNumbers, // 造成固定数值伤害
        BeDamagedToZero, // 被给予的伤害为0
    }
    public SkillEffect Effect;

    // 技能持续回合
    public int SkillLastTime = 1;
    public float[] SkillValues;

    public string SkillName;
    public string Intrduction;
}