//------------------------------------------------------------------------------
//  <copyright file="CreatureFactory.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:54:26
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using GameFramework.Event;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public class PlayerLogic : CreatureLogic
    {

        private bool canInput = false;

        private List<MonsterLogic> monsters;



        public PlayerLogic(CreatureInfo info) : base(info)
        {
            GameEntry.Event.Subscribe(BattleMonsterCommandSendEventArgs.EventId, OnMonsterBattleCommandSend);
        }

        ~PlayerLogic()
        {
            var playerView = (View as PlayerView);
            playerView.OnPointerClicked -= Player_OnPointerClicked;
            foreach (var monster in monsters)
            {
                monster.View.OnPointerClicked -= MyMonster_OnPointerClicked;
            }
            GameEntry.Event.Unsubscribe(BattleMonsterCommandSendEventArgs.EventId, OnMonsterBattleCommandSend);
        }

        private void OnMonsterBattleCommandSend(object sender, GameEventArgs e)
        {
            var ce = e as BattleMonsterCommandSendEventArgs;
            if (ce.command == GameEntry.Config.Battle.Attack)
            {
                _ = GameEntry.UI.HideBattleCommandMenu();
            }
        }

        public void SetMonsters(List<MonsterLogic> monsters)
        {
            this.monsters = monsters;
            foreach (var monster in monsters)
            {
                monster.View.OnPointerClicked += MyMonster_OnPointerClicked; ;
            }
        }

        public override void SetView(CreatureView view)
        {
            base.SetView(view);
            var playerView = (view as PlayerView);
            playerView.OnPointerClicked += Player_OnPointerClicked; ;
        }

        private void Player_OnPointerClicked(object sender, PointerEventData e)
        {
        }


        private async void MyMonster_OnPointerClicked(object sender, PointerEventData e)
        {
            var monsterView = sender as CreatureView;
            await GameEntry.UI.SetBattleCommandMenuTo(monsterView);
        }

        public async Task DisableAction()
        {
            canInput = false;
        }

        public async Task EnableAction()
        {
            canInput = true;
        }

    }
}