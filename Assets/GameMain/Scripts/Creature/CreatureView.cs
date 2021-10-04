//------------------------------------------------------------------------------
//  <copyright file="CreatureEneity.cs" company="MGO">
//  作者:  circXsora
//  邮箱:  circXsora@outlook.com
//  日期:  2021/8/21 16:45:09
//  项目:  邦邦游戏王
//  功能:
//  </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BBYGO
{
    public abstract class CreatureView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        private MaterialComponent.MaterialChanger changer;
        public MaterialComponent.MaterialChanger MaterialChanger => changer;

        public CreatureBindings Bindings { get; set; }

        private void Awake()
        {
            Bindings = GetComponent<CreatureBindings>();
            changer = GameEntry.Material.GetMaterialChanger(Bindings.mainRenderer);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameEntry.Event.OnViewPointerClick.Raise(this, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GameEntry.Event.OnViewPointerExit.Raise(this, eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GameEntry.Event.OnViewPointerEnter.Raise(this, eventData);
        }

        public virtual async Task Show()
        {
            gameObject.SetActive(true);
        }
        public virtual async Task Hide()
        {
            gameObject.SetActive(false);
        }
        public virtual async Task Destroy()
        {
            Destroy(gameObject);
        }


    }
}