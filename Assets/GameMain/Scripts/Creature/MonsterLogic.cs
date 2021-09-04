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
        private bool selectable = false;
        private MaterialComponent.MaterialChanger changer;
        public MonsterLogic(CreatureInfo info) : base(info)
        {
        }
        
        public override void SetView(CreatureView view)
        {
            base.SetView(view);
            view.OnPointerEntered += OnPointerEntered;
            view.OnPointerExited += OnPointerExited;
            view.OnPointerClicked += OnPointerClicked;
            changer = GameEntry.Material.GetMaterialChanger(view.Bindings.mainRenderer);
        }

        private void OnPointerClicked(object sender, PointerEventData e)
        {
            if (selectable)
            {

            }
        }

        private void OnPointerExited(object sender, PointerEventData e)
        {
            if (selectable)
            {
                changer.ChangeTo(MaterialComponent.MaterialType.Origin);
            }
        }

        private void OnPointerEntered(object sender, PointerEventData e)
        {
            if (selectable)
            {
                changer.ChangeTo(MaterialComponent.MaterialType.Outline);
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