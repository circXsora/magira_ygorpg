using BBYGO;
using NodeCanvas.Framework;
using System.Threading.Tasks;
using ParadoxNotion.Design;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

    public class InitBattle : ActionTask
    {
        private BattleContext battleContext;
        public EventSO InitBattleCompleteEvent;
        public async System.Threading.Tasks.Task Load()
        {
            battleContext.Init();
            var loadEnvTask = GameEntry.Environment.Load(EnvironmentType.Environment_1);
            battleContext.player = GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Player }) as PlayerLogic;
            battleContext.playerMonsters.Add(GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Monsters, entryId = 1 }) as MonsterLogic);
            battleContext.playerMonsters.Add(GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Monsters, entryId = 2 }) as MonsterLogic);
            battleContext.enemyMonsters.Add(GameEntry.Creatures.Load(new CreatureInfo() { type = CreaturesType.Monsters, entryId = 3 }) as MonsterLogic);
             
            battleContext.environment = await loadEnvTask;
            var contexts = GameEntry.Environment.GetEnvironmentContext(EnvironmentType.Environment_1);
            var context = contexts[0];

            battleContext.player.SetPoint(context.GetPlayerPoint(0));
            for (int i = 0; i < battleContext.playerMonsters.Count; i++)
            {
                battleContext.playerMonsters[i].SetPoint(context.GetPlayerMonsterPoint(i));
            }
            for (int i = 0; i < battleContext.enemyMonsters.Count; i++)
            {
                battleContext.enemyMonsters[i].SetPoint(context.GetEnemyPoint(i));
            }

            battleContext.player.SetMonsters(battleContext.playerMonsters);
            await battleContext.player.Show();
            for (int i = 0; i < battleContext.playerMonsters.Count; i++)
            {
                await battleContext.playerMonsters[i].Show();
            }
            for (int i = 0; i < battleContext.enemyMonsters.Count; i++)
            {
                await battleContext.enemyMonsters[i].Show();
            }

            InitBattleCompleteEvent?.Raise(this, battleContext);
        }

        protected override void OnExecute()
        {
            battleContext = GameEntry.Context.Battle;
            battleContext.Init();
            _ = Load();
            base.OnExecute();
        }
    }
}