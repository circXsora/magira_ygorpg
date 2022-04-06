//------------------------------------------------------------------------------
//  <copyright file="MenuForm.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/7 15:11:24
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using MGO;
namespace BBYGO
{
    public class MonsterCommandMenuForm : UIForm
    {
        public Button AttackButton;
        public Button DefendButton;
        public Button SkillButton;
        public Button EscapeButton;

        [SerializeField]
        private EventSO attackCommandEvent;
        [SerializeField]
        private EventSO defendCommandEvent;
        [SerializeField]
        private EventSO skillCommandEvent;
        [SerializeField]
        private EventSO escapeCommandEvent;

        public RectTransform Panel;

        private CreatureView currentView;

        protected override async Task ShowCore()
        {
            SetToView(GameEntry.Context.Battle.pointerClickedMonster.GetComponent<CreatureView>());
            await base.ShowCore();
        }

        public void SetToView(CreatureView view)
        {
            if (currentView != view)
            {
                currentView = view;
                try
                {
                    Panel.anchoredPosition = PositionHelper.WorldPos2CanvasPos(view.Bindings.CommandMenuPoint.position);
                }
                catch (Exception)
                {
                    throw;
                }
                AttackButton.onClick.AddListener(() =>
                {
                    attackCommandEvent?.Raise(this, null);
                });

                DefendButton.onClick.AddListener(() =>
                {
                    defendCommandEvent?.Raise(this, null);
                });

                SkillButton.onClick.AddListener(() =>
                {
                    skillCommandEvent?.Raise(this, null);
                });

                EscapeButton.onClick.AddListener(() =>
                {
                    escapeCommandEvent?.Raise(this, null);
                });
            }
        }
    }
}