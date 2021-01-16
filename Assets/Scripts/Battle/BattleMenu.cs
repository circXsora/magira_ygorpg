using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleMenu : MonoBehaviour
{
    public void SetSkillDataProvider(MonsterController monster)
    {

        SpecialSkillList.SetSkills(monster.Data.SpecialSkills);
        BattleSkillList.SetSkills(monster.Data.BattleSkill);

    }

    public SkillList SpecialSkillList, BattleSkillList;

    public event Action OnAttack, OnDefense;

    private void OnDisable()
    {
        SpecialSkillList.gameObject.SetActive(false);
        BattleSkillList.gameObject.SetActive(false);
    }

    public void HandleBattle(string action)
    {
        switch (action)
        {
            case "Attack":
                SpecialSkillList.gameObject.SetActive(false);
                BattleSkillList.gameObject.SetActive(false);
                OnAttack?.Invoke();
                break;
            case "Defense":
                SpecialSkillList.gameObject.SetActive(false);
                BattleSkillList.gameObject.SetActive(false);
                OnDefense?.Invoke();
                break;
            case "SpecialSkill":
                SpecialSkillList.gameObject.SetActive(true);
                BattleSkillList.gameObject.SetActive(false);
                break;
            case "BattleSkill":
                SpecialSkillList.gameObject.SetActive(false);
                BattleSkillList.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
