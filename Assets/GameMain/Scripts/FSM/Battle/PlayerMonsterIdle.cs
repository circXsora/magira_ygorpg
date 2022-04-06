using BBYGO;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [ParadoxNotion.Design.Category("Battle")]
    public class PlayerMonsterIdle : ActionTask
    {
        private BattleContext battleContext;
        [RequiredField]
        public EventSO onAllPlayerMonstersActionDone;
        protected override void OnExecute()
        {
            battleContext = GameEntry.Context.Battle;
            //GameEntry.Event.OnEntityPointerClick.AddListener(OnEntityPointerClick);
            //GameEntry.Event.OnEntityPointerEnter.AddListener(OnEntityPointerEnter);
            //GameEntry.Event.OnEntityPointerExit.AddListener(OnEntityPointerExit);
            battleContext.alreadyPlayerMonsterHovered = false;
            //battleContext.playerMonsters.ForEach(m => m.Selectable = !battleContext.monsterBattleTurnDatas[m].actionDone);
            if (battleContext.playerMonsters.All(m => battleContext.monsterBattleTurnDatas[m].actionDone))
            {
                onAllPlayerMonstersActionDone?.Raise(this, null);
            }
        }

        protected override void OnStop()
        {
            battleContext.playerMonsters.ForEach(m =>
            {
                //m.Selectable = false;
                //m.View.MaterialChanger.ChangeTo(MaterialComponent.MaterialType.Origin);
            });
            //GameEntry.Event.OnEntityPointerClick.RemoveListener(OnEntityPointerClick);
            //GameEntry.Event.OnEntityPointerEnter.RemoveListener(OnEntityPointerEnter);
            //GameEntry.Event.OnEntityPointerExit.RemoveListener(OnEntityPointerExit);
            _ = GameEntry.UI.Close(UIType.MonsterCommandMenuForm);
        }

        private void OnEntityPointerClick(object sender, object e)
        {
            var entity = (sender as CreatureEntity);
            if (entity == null)
            {
                Debug.LogError($"Clicked obj is not Entity, but {sender.GetType()}");
            }
            if (battleContext.pointerClickedMonster != entity)
            {
                if (battleContext.pointerClickedMonster != null)
                {
                    battleContext.pointerClickedMonster.GetComponentHolder().Get<MaterialChanger>().ChangeTo(MaterialType.Origin);
                }
                battleContext.pointerClickedMonster = entity as MonsterEntity;
                _ = GameEntry.UI.Open(UIType.MonsterCommandMenuForm);
            }
        }

        private void OnEntityPointerExit(object sender, object e)
        {
            (sender as CreatureEntity).GetComponentHolder().Get<MaterialChanger>().ChangeTo(MaterialType.Origin);
        }

        private void OnEntityPointerEnter(object sender, object data)
        {
            (sender as CreatureEntity).GetComponentHolder().Get<MaterialChanger>().ChangeTo(MaterialType.Outline);
        }
    }
}