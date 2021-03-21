using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillList : MonoBehaviour
{
    public GameObject SkillButtonItmePrefab;
    public class SkillTriggerEvent : UnityEvent<Skill>
    {

    }
    public SkillTriggerEvent OnSkillTrigger = new SkillTriggerEvent();

    List<SkillButton> skill_buttons = new List<SkillButton>();

    public void SetSkills(Skill[] skills)
    {
        foreach (var skbtn in skill_buttons)
        {
            Destroy(skbtn.gameObject);
        }
        skill_buttons.Clear();
        foreach (var skill in skills)
        {
            var skillBtn = Instantiate(SkillButtonItmePrefab, transform).GetComponent<SkillButton>();
            skillBtn.gameObject.GetComponent<Button>().onClick.AddListener(() => OnSkillTrigger?.Invoke(skill));
            skill_buttons.Add(skillBtn);
            skillBtn.TheSkill = skill;
        }
    }
}
