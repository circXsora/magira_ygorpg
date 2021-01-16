using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IMonsterHUDDataProvider
{
    event Action<MonsterHUD.HUDViewModel> OnDataChanged;
}

public class MonsterHUD : MonoBehaviour
{

    public void RegisterDataProvider(IMonsterHUDDataProvider provider)
    {
        IsRegister = true;
        RegisteredMonster = provider;
        provider.OnDataChanged += UpdateViewModel;
    }

    public void UpdateViewModel(HUDViewModel vm)
    {
        ViewModel = vm;
    }

    public HUDViewModel ViewModel
    {
        set
        {
            if (value == null)
            {
                gameObject.SetActive(false);
                HealthBar.ViewModel = null;
                if (ShowBattleSkillPanel)
                {
                    BattleSkillPanel.ViewModel = null;
                }
            }
            else
            {
                MonsterNameText.text = value.Name;
                AvatarImage.sprite = value.Avatar;
                HealthBar.ViewModel = value.HealthBarViewModel;
                if (ShowBattleSkillPanel)
                    BattleSkillPanel.ViewModel = value.BattleSkillPanelViewModel;

                gameObject.SetActive(true);
            }
        }
    }

    public class HUDViewModel
    {
        public Sprite Avatar;
        public string Name;
        public HealthBar.HealthBarViewModel HealthBarViewModel;
        public BattleSkillPanel.BattleSkillPanelViewModel BattleSkillPanelViewModel;
    }

    public Image AvatarImage;

    public TMPro.TMP_Text MonsterNameText;

    public HealthBar HealthBar;
    public bool ShowBattleSkillPanel = true;
    public BattleSkillPanel BattleSkillPanel;

    public bool IsRegister = false;
    public IMonsterHUDDataProvider RegisteredMonster = null;

    internal void UnregisterDataProvider()
    {
        IsRegister = false;
        RegisteredMonster.OnDataChanged -= UpdateViewModel;
        RegisteredMonster = null;
        ViewModel = null;
    }
}
