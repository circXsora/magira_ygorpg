using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour
{
    private Skill skill;
    TMPro.TMP_Text SkillText;


    public Skill TheSkill {
        get => skill;
        set {
            if (SkillText == null)
            {
                SkillText = GetComponentInChildren<TMPro.TMP_Text>();
            }
            skill = value;
            SkillText.text = skill.SkillName;
        } 
    }
    
}
