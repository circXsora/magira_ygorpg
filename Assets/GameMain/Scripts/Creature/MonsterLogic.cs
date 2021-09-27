//------------------------------------------------------------------------------
//  <copyright file="CreatureFactory.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:54:26
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public class MonsterLogic : CreatureLogic
    {
        protected MonsterInfo monsterInfo;
        private bool selectable = false;
        private MaterialComponent.MaterialChanger changer;

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
            changer = GameEntry.Material.GetMaterialChanger(view.Bindings.mainRenderer);
        }


        protected async override void OnMyViewBeClicked(CreatureViewBeExitedEventArgs e)
        {
            if (selectable)
            {

            }
        }

        protected override void OnMyViewBeEntered(CreatureViewBeExitedEventArgs e)
        {
            if (selectable)
            {
                changer.ChangeTo(MaterialComponent.MaterialType.Outline);
            }
        }

        protected override void OnMyViewBeExited(CreatureViewBeExitedEventArgs e)
        {
            if (selectable)
            {
                changer.ChangeTo(MaterialComponent.MaterialType.Origin);
            }
        }

        public async Task ShowSelectable()
        {
            selectable = true;
        }

        public async Task HideSelectable()
        {
            selectable = false;
        }
    }
}