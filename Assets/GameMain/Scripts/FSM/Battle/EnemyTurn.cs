using BBYGO;
using NodeCanvas.Framework;
using System.Linq;

namespace NodeCanvas.Tasks.Actions
{
    [ParadoxNotion.Design.Category("Battle")]
    public class EnemyTurn : ActionTask
    {
        private BattleContext battleContext;
        public EventSO onEnemyTurnComplete;
        private async System.Threading.Tasks.Task DoEnemiesAI()
        {
            //foreach (var e in battleContext.enemyMonsters)
            //{
            //    if (e.AI == null)
            //    {
            //        e.AI = new MonsterAI(e);
            //    }
            //    await e.AI.Run();
            //}
        }

        protected override void OnUpdate()
        {
            if (battleContext.enemyMonsters.All(m => battleContext.monsterBattleTurnDatas[m].actionDone))
            {
                onEnemyTurnComplete?.Raise(this, null);
            }
        }

        protected override void OnExecute()
        {
            battleContext = GameEntry.Context.Battle;
            battleContext.playerTurn = false;
            foreach (var monster in battleContext.enemyMonsters)
            {
                battleContext.monsterBattleTurnDatas[monster].actionDone = false;
            }
            _ = DoEnemiesAI();
        }
    }
}