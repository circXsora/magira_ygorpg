//------------------------------------------------------------------------------
//  <copyright file="CreatureFactory.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:54:26
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using DG.Tweening;
using MGO;
using NodeCanvas.Framework;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public class DamageInfo
    {
        public int Damage { get; set; }
    }

    [Serializable]
    public class MonsterLogic : CreatureLogic
    {
        public CreatureLogic Owner { get; private set; }
        private MonsterView monsterView;
        public MonsterView MonsterView
        {
            get
            {
                if (monsterView == null)
                {
                    monsterView = View as MonsterView;
                }
                return monsterView;
            }
        }
        public MonsterState MonsterState { get; set; }
        public MonsterEntry MonsterEntry { get; set; }

        public MonsterLogic(CreatureInfo info) : base(info)
        {
            MonsterState = new MonsterState() { PhysicalAttackRange = new IntRange(1, 3) };
        }

        public void SetOwner(CreatureLogic owner)
        {
            Owner = owner;
        }

        public override void SetView(CreatureView view)
        {
            base.SetView(view);
        }

        public async System.Threading.Tasks.Task Attack(MonsterLogic victimLogic)
        {
            var damageInfo = new DamageInfo()
            {
                Damage = MonsterState.PhysicalAttackRange.Random()
            };
            await View.PerformVisualEffect(GameEntry.Config.visualEffectType.normalAttack1, new TimePointHandler[] { new TimePointHandler() {
                start = (a,b,c)=>{ _ = victimLogic.SufferDamagePre(damageInfo); },
                realEffect = (a,b,c)=>{ _ = victimLogic.SufferDamage(damageInfo); },
                end = (a,b,c)=>{ _ = victimLogic.SufferDamagePost(damageInfo); },
            } });
        }

        public async System.Threading.Tasks.Task SufferDamagePre(DamageInfo damageInfo)
        {
            UberDebug.Log("准备遭受攻击");
        }

        public async System.Threading.Tasks.Task SufferDamagePost(DamageInfo damageInfo)
        {

        }

        public async System.Threading.Tasks.Task SufferDamage(DamageInfo damageInfo)
        {
            CreatureState.Hp = Mathf.Clamp(CreatureState.Hp - damageInfo.Damage, 0, CreatureState.MaxHp);
            await View.PerformVisualEffect(GameEntry.Config.visualEffectType.normalSufferDamage1, new TimePointHandler[] {
                new TimePointHandler()
                {
                    realEffect = (a, b, c) => { MonsterView.HPBar.ShowHP(CreatureState.Hp, CreatureState.MaxHp);},
                    end = (a,b,c)=>{ _ = GameEntry.VisualEffect.PerformNumberTextEffect(View.Bindings.DamageTextPoint.position, damageInfo.Damage, GameEntry.Config.visualEffectType.normalSufferDamage1);}
                } });
        }
    }
}