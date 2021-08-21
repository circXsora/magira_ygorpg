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
using System.Collections;
using System.Collections.Generic;
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
            EnemyTurnEnd,
            PlayerAllDead,
            EnemyAllDead,
        }

        private readonly AsyncPassiveStateMachine<States, Events> machine;

        public BattleStateMachine()
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

            builder
                .WithInitialState(States.BattleInit);

            machine = builder.Build().CreatePassiveStateMachine();

        }

        public Task Start()
        {
            return machine.Start();
        }
    }
}