//------------------------------------------------------------------------------
//  <copyright file="CreatureFactory.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:54:26
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public class PlayerLogic : CreatureLogic
    {
        private List<MonsterLogic> monsters;

        public List<MonsterLogic> Monsters => monsters;

        private bool canInput = true;

        public PlayerLogic(CreatureInfo info) : base(info)
        { }
        //public PlayerLogic(CreatureInfo info) : base(info)
        //{
        //    GameEntry.Event.Subscribe(BattleMonsterCommandSendEventArgs.EventId, OnMonsterBattleCommandSend);
        //}

        //~PlayerLogic()
        //{
        //    GameEntry.Event.Unsubscribe(BattleMonsterCommandSendEventArgs.EventId, OnMonsterBattleCommandSend);
        //}

        //private void OnMonsterBattleCommandSend(object sender, GameEventArgs e)
        //{
            //var ce = e as BattleMonsterCommandSendEventArgs;
            //if (ce.command == GameEntry.Config.Battle.Attack)
            //{
            //    _ = GameEntry.UI.HideBattleCommandMenu();
            //}
        //}

        public void SetMonsters(List<MonsterLogic> monsters)
        {
            this.monsters = monsters;
            foreach (var monster in monsters)
            {
                monster.SetOwner(this);
            }
        }

        //protected async override void OnMyViewBeClicked(CreatureViewBeExitedEventArgs e)
        //{
        //    //await GameEntry.UI.SetBattleCommandMenuTo(View);
        //}


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