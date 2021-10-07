using BBYGO;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [ParadoxNotion.Design.Category("Battle")]
    public class SelectEnemy : ActionTask
    {
        private BattleContext battleContext;

        protected override void OnExecute()
        {
            battleContext = GameEntry.Context.Battle;
            GameEntry.Event.OnViewPointerClick.AddListener(OnViewPointerClick);
            GameEntry.Event.OnViewPointerEnter.AddListener(OnViewPointerEnter);
            GameEntry.Event.OnViewPointerExit.AddListener(OnViewPointerExit);
            battleContext.enemyMonsters.ForEach(m =>
            {
                m.Selectable = true;
            });
        }

        protected override void OnStop()
        {
            GameEntry.Event.OnViewPointerClick.RemoveListener(OnViewPointerClick);
            GameEntry.Event.OnViewPointerEnter.RemoveListener(OnViewPointerEnter);
            GameEntry.Event.OnViewPointerExit.RemoveListener(OnViewPointerExit);
            battleContext.enemyMonsters.ForEach(m =>
            {
                m.Selectable = false;
                m.View.MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Origin);
            });
        }

        private void OnViewPointerClick(object sender, object e)
        {
            var view = sender as CreatureView;
            battleContext.selectMonsters.Clear();
            battleContext.selectMonsters.Add(view.gameObject);
            view.MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Origin);
        }

        private void OnViewPointerExit(object sender, object e)
        {
            (sender as CreatureView).MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Origin);
        }

        private void OnViewPointerEnter(object sender, object data)
        {
            (sender as CreatureView).MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Outline);
        }
    }
}