//------------------------------------------------------------------------------
//  <copyright file="BattleForm.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/5/15 18:53:44
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
namespace BBYGO
{
    using MGO;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityGameFramework.Runtime;

    public class BattleForm : UGuiForm
    {
        [SerializeField] private GameObject _playerHUDGroup;
        public GameObject PlayerHUDGroup { get => _playerHUDGroup; set => _playerHUDGroup = value; }

        BattleFormParams _battleFormParams;

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            _battleFormParams = userData as BattleFormParams;
            var getNextCell = UguiUtility.CreateCellGenerator(PlayerHUDGroup.transform.GetChild(0).gameObject, PlayerHUDGroup.transform);
            foreach (var data in _battleFormParams.PlayerData.MonsterDatas)
            {
                var cell = getNextCell();
                var uiControlData = cell.GetComponent<UIControlData>();

                var avatar = uiControlData.Get<Image>("Avatar");
                GameEntry.Resource.LoadSprite(data.EntryData.SpriteAssetName, (sprite) => avatar.sprite = sprite);

                var nameText = uiControlData.Get<TMPro.TMP_Text>("MonsterName");
                nameText.text = data.EntryData.Name;

                uiControlData.Get<TMPro.TMP_Text>("LevelText").text = "Lv. " + data.CurrentLevel;

                var levelData = GameEntry.DataTable.GetDataTable<DRMonsterLevel>().GetDataRow(data.EntryData.Id * 1000 + data.CurrentLevel);

                var healthBar = uiControlData.Get<Slider>("HealthBar");
                var healthText = uiControlData.Get<TMPro.TMP_Text>("HealthText");
                healthBar.value = data.CurrentHealthValue / levelData.MaxHealth;
                healthText.text = data.CurrentHealthValue + "/" + levelData.MaxHealth;

                var magicBar = uiControlData.Get<Slider>("MagicBar");
                var magicText = uiControlData.Get<TMPro.TMP_Text>("MagicText");
                if (levelData.MaxMagic != 0)
                {
                    magicBar.value = data.CurrentMagicValue / levelData.MaxMagic;
                    magicText.text = data.CurrentMagicValue + "/" + levelData.MaxMagic;
                }
                else
                {
                    magicBar.value = 0;
                    magicText.text = string.Empty;
                }

                uiControlData.Get<TMPro.TMP_Text>("PowerText").text = levelData.Power.ToString();
                uiControlData.Get<TMPro.TMP_Text>("SpeedText").text = levelData.Speed.ToString();
                uiControlData.Get<TMPro.TMP_Text>("LuckyText").text = levelData.Lucky.ToString();
                uiControlData.Get<TMPro.TMP_Text>("StaminaText").text = levelData.Stamina.ToString();
                uiControlData.Get<TMPro.TMP_Text>("WitchText").text = levelData.Witch.ToString();

                var starsPanel = uiControlData.Get<Transform>("MonsterStarsPanel");
                var getNextStarCell = UguiUtility.CreateCellGenerator(starsPanel.GetChild(0).gameObject, starsPanel);
                for (int i = 0; i < data.EntryData.Star; i++)
                {
                    var starCell = getNextStarCell();
                    starCell.SetActive(true);
                }
                var starsText = uiControlData.Get<TMPro.TMP_Text>("StarText");
                starsText.text = "x " + data.EntryData.Star;


                cell.gameObject.SetActive(true);
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
}