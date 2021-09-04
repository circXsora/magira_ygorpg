//------------------------------------------------------------------------------
//  <copyright file="BattleStateMachine.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/18 22:38:16
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.AsyncMachine;
using GameFramework.Event;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class BattleStateMachine
    {
        private enum States
        {
            BattleInit,
            BattleRunning,
            PlayerTurn,
            EnemyTurn,
            BattleOver
        }

        private enum Events
        {
            BattleInitFinished,
            PlayerTurnEnd,
            MonsterBeSelect,
            EnemyTurnEnd,
            PlayerAllDead,
            EnemyAllDead,
        }

        private AttackData currentAttackData;

        private readonly AsyncPassiveStateMachine<States, Events> machine;

        public PlayerLogic[] Players { get; }
        public MonsterLogic[] Enemies { get; }

        public BattleStateMachine(PlayerLogic[] players, MonsterLogic[] enemies)
        {
            var builder = new StateMachineDefinitionBuilder<States, Events>();

            #region Hierarchies
            builder.DefineHierarchyOn(States.BattleRunning)
    .WithHistoryType(HistoryType.None)
    .WithInitialSubState(States.PlayerTurn)
    .WithSubState(States.EnemyTurn);
            #endregion

            #region Transitions
            builder
                .In(States.BattleInit)
                .On(Events.BattleInitFinished)
                .Goto(States.BattleRunning);

            builder.In(States.PlayerTurn).On(Events.PlayerTurnEnd).Goto(States.EnemyTurn);
            builder.In(States.EnemyTurn).On(Events.EnemyTurnEnd).Goto(States.PlayerTurn);

            builder.In(States.BattleRunning).On(Events.PlayerAllDead).Goto(States.BattleOver);
            builder.In(States.BattleRunning).On(Events.EnemyAllDead).Goto(States.BattleOver);
            #endregion

            #region Actions
            builder.In(States.BattleInit).ExecuteOnEntry(async () =>
            {
                await Task.Delay(1000);
                await machine.Fire(Events.BattleInitFinished);
            });

            builder.In(States.PlayerTurn).ExecuteOnEntry(async () =>
           {
               await EnablePlayersAction();
           }).ExecuteOnExit(async () =>
           {
               await DiablePlayersAction();
           });

            #endregion

            #region Events
            builder.In(States.PlayerTurn).On(Events.MonsterBeSelect).Execute<CreatureView>((view) =>
            {
                var enemy = Enemies.First(e => e.View == view);
                if (enemy != null)
                {
                    currentAttackData.Targets.Add(enemy);
                }
            });
            #endregion

            builder
                .WithInitialState(States.BattleInit);

            machine = builder.Build().CreatePassiveStateMachine();
            Players = players;
            Enemies = enemies;

            SubscribeEvents();
        }
        ~BattleStateMachine()
        {
            UnSubscribeEvnets();
        }

        private void SubscribeEvents()
        {
            GameEntry.Event.Subscribe(BattleMonsterCommandSendEventArgs.EventId, OnMonsterBattleCommandSend);
            GameEntry.Event.Subscribe(BattleCreatureBeClickedEventArgs.EventId, OnBattleCreatureBeClicked);
        }

        private void UnSubscribeEvnets()
        {
            GameEntry.Event.Unsubscribe(BattleMonsterCommandSendEventArgs.EventId, OnMonsterBattleCommandSend);
            GameEntry.Event.Unsubscribe(BattleCreatureBeClickedEventArgs.EventId, OnBattleCreatureBeClicked);
        }

        private void OnBattleCreatureBeClicked(object sender, GameEventArgs e)
        {
            var @evnet = e as BattleCreatureBeClickedEventArgs;
            _ = machine.Fire(Events.MonsterBeSelect, evnet.view);
        }

        private void OnMonsterBattleCommandSend(object sender, GameEventArgs e)
        {
            var me = e as BattleMonsterCommandSendEventArgs;
            if (me.command == GameEntry.Config.Battle.Attack)
            {
                currentAttackData = new AttackData();
                //Players.Select(p=>p.GetMonsters())
                //currentAttackData.Player =;
                _ = EnableEnemySelectable();
            }
        }

        private async Task EnablePlayersAction()
        {
            foreach (var player in Players)
            {
                await player.EnableAction();
            }
        }

        private async Task DiablePlayersAction()
        {
            foreach (var player in Players)
            {
                await player.DisableAction();
            }
        }

        private async Task EnableEnemySelectable()
        {
            foreach (var enemy in Enemies)
            {
                await enemy.ShowSelectable();
            }
        }

        private async Task DisableEnemySelectable()
        {
            foreach (var enemy in Enemies)
            {
                await enemy.HideSelectable();
            }
        }

        public Task Start()
        {
            return machine.Start();
        }
    }
}