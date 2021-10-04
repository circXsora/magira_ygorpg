//------------------------------------------------------------------------------
//  <copyright file="CreatureFactory.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:54:26
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using NodeCanvas.Framework;
using System;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public class MonsterLogic : CreatureLogic
    {
        protected MonsterInfo monsterInfo;
        protected Blackboard blackboard;

        public MonsterLogic(CreatureInfo info) : this(info, new MonsterInfo())
        {
        }

        public MonsterLogic(CreatureInfo info, MonsterInfo monsterInfo) : base(info)
        {
            this.monsterInfo = monsterInfo;
        }

        public void SetOwner(CreatureLogic owner)
        {
            monsterInfo.owner = owner;
        }

        public override void SetView(CreatureView view)
        {
            base.SetView(view);
            blackboard = view.GetComponent<Blackboard>();
            blackboard.SetVariableValue(nameof(MaterialComponent.MaterialChanger), view.MaterialChanger);
            blackboard.SetVariableValue("SenderCompareObject", view);
        }
    }
}