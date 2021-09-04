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
using UnityGameFramework.Runtime;
using MGO;
namespace BBYGO
{
    public class MonsterCommandMenuForm : SoraUIForm
    {
        public Button AttackButton;
        public Button DefendButton;
        public Button SkillButton;
        public Button EscapeButton;

        public RectTransform Panel;

        private CreatureView currentView;

        public void SetToView(CreatureView view)
        {
            if (currentView != view)
            {
                currentView = view;
                Panel.anchoredPosition = PositionHelper.WorldPos2CanvasPos(view.Bindings.CommandMenuPoint.position);
                AttackButton.onClick.AddListener(() =>
                {
                    GameEntry.Event.Raise(this, BattleMonsterCommandSendEventArgs.Create(view, GameEntry.Config.Battle.Attack));
                });

                DefendButton.onClick.AddListener(() =>
                {
                    GameEntry.Event.Raise(this, BattleMonsterCommandSendEventArgs.Create(view, GameEntry.Config.Battle.Defend));
                });

                SkillButton.onClick.AddListener(() =>
                {
                    GameEntry.Event.Raise(this, BattleMonsterCommandSendEventArgs.Create(view, GameEntry.Config.Battle.Skill));
                });

                EscapeButton.onClick.AddListener(() =>
                {
                    GameEntry.Event.Raise(this, BattleMonsterCommandSendEventArgs.Create(view, GameEntry.Config.Battle.Escape));
                });
            }
        }
    }
}