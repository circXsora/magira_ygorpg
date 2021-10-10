using BBYGO;
using NodeCanvas.Framework;
using System.Threading.Tasks;
using ParadoxNotion.Design;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [ParadoxNotion.Design.Category("Battle")]
    public class InitPlayerTurn : ActionTask
    {
        private BattleContext battleContext;

        protected override void OnExecute()
        {
            battleContext = GameEntry.Context.Battle;
            battleContext.playerTurn = true;
            foreach (var monster in battleContext.playerMonsters)
            {
                battleContext.monsterBattleTurnDatas[monster].actionDone = false;
            }
            EndAction(true);
        }
    }
}