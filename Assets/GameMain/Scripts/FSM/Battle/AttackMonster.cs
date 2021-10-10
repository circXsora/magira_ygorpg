using BBYGO;
using DG.Tweening;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [ParadoxNotion.Design.Category("Battle")]
    public class AttackMonster : ActionTask
    {
        private BattleContext battleContext;
        public float moveTime = 0.5f;
        public float attackTime = 0.5f;
        public float backTime = 0.2f;
        protected override void OnExecute()
        {
            battleContext = GameEntry.Context.Battle;
            battleContext.enemyMonsters.ForEach(m =>
            {
                m.Selectable = false;
            });

            battleContext.playerMonsters.ForEach(m =>
            {
                m.Selectable = false;
            });

            var seq = DOTween.Sequence();
            var originPos = battleContext.pointerClickedMonster.transform.position;
            seq.Append(battleContext.pointerClickedMonster.transform.DOMove(battleContext.selectMonsters[0].transform.position, moveTime));
            seq.Append(battleContext.pointerClickedMonster.transform.DOShakePosition(attackTime));
            seq.Append(battleContext.pointerClickedMonster.transform.DOMove(originPos, backTime));
            var creature = GameEntry.Creatures.GetCreatureLogicByGameObjerct(battleContext.pointerClickedMonster);
            battleContext.monsterBattleTurnDatas[creature as MonsterLogic].actionDone = true;
            seq.OnComplete(() => EndAction(true));
        }

        protected override void OnUpdate()
        {
            
        }

        protected override void OnStop()
        {

        }
    }
}