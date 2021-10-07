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
    public class PlayerMonsterIdle : ActionTask
    {
        private BattleContext battleContext;

        protected override void OnExecute()
        {
            battleContext = GameEntry.Context.Battle;
            GameEntry.Event.OnViewPointerClick.AddListener(OnViewPointerClick);
            GameEntry.Event.OnViewPointerEnter.AddListener(OnViewPointerEnter);
            GameEntry.Event.OnViewPointerExit.AddListener(OnViewPointerExit);
            battleContext.playerMonsters.ForEach(m =>
            {
                m.Selectable = true;
                m.View.MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Origin);
            });
            battleContext.alreadyPlayerMonsterHovered = false;
        }

        protected override void OnStop()
        {
            battleContext.playerMonsters.ForEach(m =>
            {
                m.Selectable = false;
                m.View.MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Origin);
            });
            GameEntry.Event.OnViewPointerClick.RemoveListener(OnViewPointerClick);
            GameEntry.Event.OnViewPointerEnter.RemoveListener(OnViewPointerEnter);
            GameEntry.Event.OnViewPointerExit.RemoveListener(OnViewPointerExit);
            _ = GameEntry.UI.Close(UIType.MonsterCommandMenuForm);
        }

        private void OnViewPointerClick(object sender, object e)
        {
            var view = (sender as CreatureView);
            if (battleContext.pointerClickedMonster != view.gameObject)
            {
                battleContext.pointerClickedMonster?.GetComponent<CreatureView>().MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Origin);
                battleContext.pointerClickedMonster = view.gameObject;
                _ = GameEntry.UI.Open(UIType.MonsterCommandMenuForm);
            }
        }

        private void OnViewPointerExit(object sender, object e)
        {
            var view = (sender as CreatureView);
            if (battleContext.pointerClickedMonster != view.gameObject)
            {
                view.MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Origin);
            }
        }

        private void OnViewPointerEnter(object sender, object data)
        {
            (sender as CreatureView).MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Outline);
        }
    }
}