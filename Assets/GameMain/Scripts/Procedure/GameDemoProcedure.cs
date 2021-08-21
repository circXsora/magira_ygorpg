//------------------------------------------------------------------------------
//  <copyright file="GameProcedure.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/7 15:35:49
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BBYGO
{
    public class GameDemoProcedure : SoraProcedureBase
    {
        [ReadOnly, SerializeField]
        private CreatureLogic playerLogic, enemyLogic;

        public async override void OnEnter()
        {
            base.OnEnter();
            var loadEnvTask = GameEntry.Environment.Load(EnvironmentType.Environment_1);
            var loadPlayerTask = GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Player });
            var loadEnemyTask = GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Monster });

            await loadEnvTask;

            var contexts = GameEntry.Environment.GetEnvironmentContext(EnvironmentType.Environment_1);
            var context = contexts[0];

            playerLogic = await loadPlayerTask;
            enemyLogic = await loadEnemyTask;
            playerLogic.SetPoint(context.GetPlayerPoint(0));
            enemyLogic.SetPoint(context.GetEnemyPoint(0));

            await playerLogic.Show();
            await enemyLogic.Show();

            RunBattleLogic();
        }

        public void RunBattleLogic()
        {
            var stateMachine = new BattleStateMachine();
            stateMachine.Start();
        }
    }
}