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
            //GameEntry.Event.OnEntityPointerClick.AddListener(OnViewPointerClick);
            //GameEntry.Event.OnEntityPointerEnter.AddListener(OnViewPointerEnter);
            //GameEntry.Event.OnEntityPointerExit.AddListener(OnViewPointerExit);
            battleContext.enemyMonsters.ForEach(m =>
            {
                //m.Selectable = true;
            });
        }

        protected override void OnStop()
        {
            //GameEntry.Event.OnEntityPointerClick.RemoveListener(OnViewPointerClick);
            //GameEntry.Event.OnEntityPointerEnter.RemoveListener(OnViewPointerEnter);
            //GameEntry.Event.OnEntityPointerExit.RemoveListener(OnViewPointerExit);
            battleContext.enemyMonsters.ForEach(m =>
            {
                //m.Selectable = false;
                //m.View.MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Origin);
            });
        }

        private void OnViewPointerClick(object sender, object e)
        {
            var entity = sender as MonsterEntity;
            battleContext.selectMonsters.Clear();
            battleContext.selectMonsters.Add(entity);
            entity.GetComponentHolder().Get<MaterialChanger>().ChangeTo(MaterialType.Origin);
        }

        private void OnViewPointerExit(object sender, object e)
        {
            var entity = sender as CreatureEntity;
            entity.GetComponentHolder().Get<MaterialChanger>().ChangeTo(MaterialType.Origin);
        }

        private void OnViewPointerEnter(object sender, object data)
        {
            var entity = sender as CreatureEntity;
            entity.GetComponentHolder().Get<MaterialChanger>().ChangeTo(MaterialType.Origin);
        }
    }
}