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
        protected override async void OnExecute()
        {
            battleContext = GameEntry.Context.Battle;
            battleContext.enemyMonsters.ForEach(m =>
            {
                //m.Selectable = false;
            });

            battleContext.playerMonsters.ForEach(m =>
            {
                //m.Selectable = false;
            });

            var seq = DOTween.Sequence();

            var attacker = battleContext.pointerClickedMonster;
            var attackerEntity = attacker.GetComponent<MonsterEntity>();

            var victim = battleContext.selectMonsters[0];
            var victimEntity = victim.GetComponent<MonsterEntity>();

            var originPos = attacker.transform.position;
            var targetPos = originPos * 0.2f + victim.transform.position * 0.8f;
            await attacker.transform.DOMove(targetPos, moveTime).AsyncWaitForCompletion();
            await attackerEntity.MonsterLogic.Attack(victimEntity.MonsterLogic);
            await attacker.transform.DOMove(originPos, backTime).AsyncWaitForCompletion();
            battleContext.monsterBattleTurnDatas[attackerEntity].actionDone = true;
            EndAction(true);
        }
    }
}