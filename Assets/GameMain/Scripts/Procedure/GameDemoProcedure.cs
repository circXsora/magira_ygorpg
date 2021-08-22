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
        private CreatureLogic playerLogic;
        [ReadOnly, SerializeField]
        private CreatureLogic enemyLogic;
        [ReadOnly, SerializeField]
        private CreatureLogic playerMonster1Logic;
        [ReadOnly, SerializeField]
        private CreatureLogic playerMonster2Logic;

        public async override void OnEnter()
        {
            base.OnEnter();
            var loadEnvTask = GameEntry.Environment.Load(EnvironmentType.Environment_1);
            var loadPlayerTask = GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Player });
            var loadPlayerMonster1Task = GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Monsters, entryId = 1 });
            var loadPlayerMonster2Task = GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Monsters, entryId = 2 });

            var loadEnemyMonsterTask = GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Monsters, entryId = 3 });

            await loadEnvTask;

            var contexts = GameEntry.Environment.GetEnvironmentContext(EnvironmentType.Environment_1);
            var context = contexts[0];

            playerLogic = await loadPlayerTask;
            playerMonster1Logic = await loadPlayerMonster1Task;
            playerMonster2Logic = await loadPlayerMonster2Task;
            enemyLogic = await loadEnemyMonsterTask;

            playerLogic.SetPoint(context.GetPlayerPoint(0));
            playerMonster1Logic.SetPoint(context.GetPlayerMonsterPoint(0));
            playerMonster2Logic.SetPoint(context.GetPlayerMonsterPoint(1));
            enemyLogic.SetPoint(context.GetEnemyPoint(0));

            await playerLogic.Show();
            await playerMonster1Logic.Show();
            await playerMonster2Logic.Show();
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