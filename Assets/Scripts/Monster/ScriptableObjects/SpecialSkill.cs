using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "New Special Skill", menuName = "Skill/New Special Skill")]
[System.Serializable]
public class SpecialSkill : Skill
{
    public enum UseRange
    {
        One,
        Many,
        OneTurn,
        Death // 死亡时发动
    }
    public UseRange Use;
} 
