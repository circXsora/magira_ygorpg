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
    [Serializable]
    public class MonsterLogic : CreatureLogic
    {
        public CreatureLogic Owner { get; private set; }

        public MonsterLogic(CreatureInfo info) : base(info)
        {

        }

        public void SetOwner(CreatureLogic owner)
        {
            Owner = owner;
        }

        public override void SetView(CreatureView view)
        {
            base.SetView(view);
        }
    }
}