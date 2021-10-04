using BBYGO;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{

    public class PlayerTurn : ActionTask
    {
        private BattleContext battleContext;

        protected override void OnExecute()
        {
            base.OnExecute();
            battleContext = GameEntry.Context.Battle;
        }
    }
}