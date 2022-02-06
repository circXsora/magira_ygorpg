using BBYGO;
using NodeCanvas.Framework;
using System.Threading.Tasks;
using ParadoxNotion.Design;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.Linq;

namespace NodeCanvas.Tasks.Actions
{
    [ParadoxNotion.Design.Category("Battle")]
    public class InitBattle : ActionTask
    {
        private BattleContext battleContext;
        public EventSO InitBattleCompleteEvent;
        public async System.Threading.Tasks.Task Load()
        {
            var loadEnvTask = GameEntry.Environment.Load(EnvironmentType.Environment_1);
            var creature = GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Player });
            if (creature is PlayerEntity player)
            {
                battleContext.player = player;
            }
            else
            {
                Debug.LogError("创建失败，创建的类型不是Player而是" + creature.GetType());
            }
            battleContext.player.Init();
            battleContext.playerMonsters.Add(GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Monsters, entryId = 1, party = CreaturesParty.Player }) as MonsterEntity);
            battleContext.playerMonsters.Add(GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Monsters, entryId = 2, party = CreaturesParty.Player }) as MonsterEntity);
            battleContext.enemyMonsters.Add(GameEntry.Creatures.CreateCreature(new CreatureInfo() { type = CreaturesType.Monsters, entryId = 3, party = CreaturesParty.Enemy }) as MonsterEntity);

            battleContext.environment = await loadEnvTask;
            var envContext = battleContext.environment.GetContext(0);

            battleContext.player.SetPoint(envContext.GetPlayerPoint(0));
            for (int i = 0; i < battleContext.playerMonsters.Count; i++)
            {
                battleContext.playerMonsters[i].SetPoint(envContext.GetPlayerMonsterPoint(i));
                battleContext.monsterBattleTurnDatas.Add(battleContext.playerMonsters[i], new BattleContext.BattleTurnData());
                battleContext.playerMonsters[i].Init();
            }
            for (int i = 0; i < battleContext.enemyMonsters.Count; i++)
            {
                battleContext.enemyMonsters[i].SetPoint(envContext.GetEnemyPoint(i));
                battleContext.monsterBattleTurnDatas.Add(battleContext.enemyMonsters[i], new BattleContext.BattleTurnData());
                battleContext.enemyMonsters[i].Init();
            }

            battleContext.player.PlayerLogic.SetMonsters(battleContext.playerMonsters.Select(m => m.MonsterLogic).ToList());
            await battleContext.player.Active();
            for (int i = 0; i < battleContext.playerMonsters.Count; i++)
            {
                await battleContext.playerMonsters[i].Active();
            }
            for (int i = 0; i < battleContext.enemyMonsters.Count; i++)
            {
                await battleContext.enemyMonsters[i].Active();
            }

            InitBattleCompleteEvent?.Raise(this, battleContext);
        }

        protected override void OnExecute()
        {
            battleContext = GameEntry.Context.Battle;
            battleContext.Init();
            _ = Load();
        }
    }
}